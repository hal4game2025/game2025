using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;
using MySystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField][Tooltip("ベースのパワー")] float swingForce = 10f;//ぱわー
    [SerializeField] Transform cameraLook;
    [SerializeField][Tooltip("コンボが増えたときに早くなるペース")] float adjustSwingForce = 1.0f;//コンボが増えたときに早くなるペース
    [SerializeField] bool directionReverse = false;// trueなら入力方向を反転
    [SerializeField] bool processOnlyOnCollision = false; // trueなら壁か敵に当たったときだけ
    [SerializeField] int max_speed_coef = 1;
    [SerializeField] PlayerMovement playerMovement;  //プレイヤーの動き


    [CustomLabel("最小ヒットストップ時間")]
    [SerializeField] float baseHitStopTime = 0.1f; //コンボ数が0の時のベースヒットストップ時間
    [CustomLabel("最大ヒットストップ時間")]
    [SerializeField] float maxHitStopTime = 0.5f; //最大ヒットストップ時間
    [CustomLabel("ヒットストップのコンボ倍率")]
    [SerializeField] float perComboHitStopTime = 0.05f; //コンボ数に応じ、だんだんヒットストップ時間が増える


    EnemyStatus[] enemyList; //衝突した敵のリスト

    HammerCollision hammerCollision; //はんまーの当たり判定
    PlayerControls controls;         //入力アクション
    Vector2 inputDirection;          //入力方向
    PlayerStatus playerStatus;       //プレイヤーの状態
    PlayerAnim playerAnim;           //プレイヤーのアニメーション
    Coroutine hitStopCoroutine;      //ヒットストップのこるーちん

    bool isHitStop = false;         //ヒットストップ中かどうか

    [SerializeField]
    SoundManager soundManager;
    [SerializeField]
    pair<string,AudioClip>[] playerSE;  // インスペクター設定用

    Dictionary<string, AudioClip> seList;

    [SerializeField]
    CameraMovement playerCamera;
    

    //追加
    EnemyStatus enemyStatus;         //敵のステータス
    public EnemyStatus EnemyStatus
    {
        get { return enemyStatus; }
        set { enemyStatus = value; }
    }

    Vector3 lookDirection;
    void Start()
    {
        

        // マウス固定＆非表示
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        hammerCollision = gameObject.transform.GetChild(0).GetComponent<HammerCollision>(); //こども（はんまーの当たり判定）取得
        playerStatus = GetComponent<PlayerStatus>();
        playerAnim = GetComponent<PlayerAnim>();
        controls = new PlayerControls();
        playerMovement = new PlayerMovement(GetComponent<Rigidbody>(), adjustSwingForce);
        playerMovement.max_coef = max_speed_coef;

        // 入力アクション
        controls.Player.Direction.performed += OnDirection;
        controls.Player.Direction.canceled += OnDirection;
        controls.Player.HammerSwing_LR_UD.performed += OnHammerSwing;
        controls.Player.Trun.performed += ONTurn;
        controls.Player.HammerSwingMoveForward.performed += OnHammerSwingMoveForward;
        controls.Enable();

        seList = new Dictionary<string, AudioClip>();
        foreach (pair<string,AudioClip> temp in playerSE)
        {
            seList.Add(temp.Key, temp.Value);
        }

        //敵のリストを取得
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        List<EnemyStatus> enemyStatusList = new List<EnemyStatus>();
        foreach (var obj in enemyObjects)
        {
            EnemyStatus status = obj.GetComponent<EnemyStatus>();
            if (status != null)
            {
                enemyStatusList.Add(status);
            }
        }
        enemyList = enemyStatusList.ToArray();
    }

    void Update()
    {
        //カメラの向きに合わせてプレイヤーを回転させる
        FaceCameraDirection();

        //デバッグ用
        //text.text = "コンボ数:" + playerStatus.Combo;

        //デバッグ用コンボ数増減
        if (Input.GetKeyDown(KeyCode.P))
        {
            playerStatus.Combo++;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            playerStatus.Combo--;
        }

        Debug.Log("rate:" + playerStatus.Rate);
    }

    /// <summary>
    /// ハンマーで飛ぶ。壁か的だったらコンボ数増やす
    /// </summary>
    /// <param name="context"></param>
    void OnHammerSwing(InputAction.CallbackContext context)
    {
        //スタン状態か、ヒットストップ状態なら処理しない
        if (playerStatus.IsStunned ||
            isHitStop)
            return;

        //壁か敵or空中判定
        if (hammerCollision.IsColliding())
        {

            UpdatePlayerStatus();

            playerMovement.SwingHammer(inputDirection, cameraLook, swingForce, playerStatus.Combo);
            playerAnim.SetAnimationByDirection(inputDirection);

            lookDirection = playerMovement.ReturnDirection(inputDirection, cameraLook.rotation);//カメラの向きに合わせた方向を取得



            // 共通するEnemyStatusだけにダメージ
            var hammerEnemies = hammerCollision.GetEnemyStatusList();
            if (hammerEnemies != null && enemyList != null)
            {
                for (int i = 0; i < enemyStatusList.Count; i++)
                {
                    SoundManager.Instance.Play(seList["attack"]);
                    enemyStatusList[i].Damage(playerStatus.Rate);
                foreach (var enemy in hammerEnemies)
                {
                    // enemyList（配列）に含まれているかチェック
                    if (System.Array.Exists(enemyList, e => e == enemy))
                    {
                        enemy.Damage(playerStatus.Rate);
                    }
                }
            }

            //ヒットストップ開始
            StartHitStop(playerStatus.Combo);
            Debug.Log("壁か敵殴った");
            soundManager.Play(seList["swing"]);
        }
        else if (playerStatus.CanAirMove)
        {
            // 空中のジャンプ回数を数える
            playerStatus.IncrementAirMoveCount();

            playerMovement.SwingHammer(inputDirection, cameraLook, swingForce, playerStatus.Combo);
            playerAnim.SetAnimationByDirection(inputDirection);
            lookDirection = playerMovement.ReturnDirection(inputDirection, cameraLook.rotation);//カメラの向きに合わせた方向を取得
            Debug.Log("空気殴った");
            soundManager.Play(seList["swing"]);

        }

        

    }

    void OnHammerSwingMoveForward(InputAction.CallbackContext context)
    {
        //スタン状態か、ヒットストップ状態なら処理しない
        if (playerStatus.IsStunned || isHitStop)
            return;

        //壁か敵or空中判定
        if (hammerCollision.IsColliding())
        {
            UpdatePlayerStatus();

            playerMovement.SwingHammerMoveForward(playerCamera.transform.forward,swingForce, playerStatus.Combo);
            playerAnim.SetAnimationByCameraForward();
            lookDirection = playerMovement.ReturnDirectionForward(cameraLook.rotation);//カメラの向きに合わせた方向を取得


            // 共通するEnemyStatusだけにダメージ
            var hammerEnemies = hammerCollision.GetEnemyStatusList();
            if (hammerEnemies != null && enemyList != null)
            {
                foreach (var enemy in hammerEnemies)
                {
                    SoundManager.Instance.Play(seList["attack"]);
                    enemyStatusList[i].Damage(playerStatus.Rate);
                    // enemyList（配列）に含まれているかチェック
                    if (System.Array.Exists(enemyList, e => e == enemy))
                    {
                        enemy.Damage(playerStatus.Rate);
                    }
                }
            }

            //ヒットストップ開始
            StartHitStop(playerStatus.Combo);
            Debug.Log("壁か敵殴った");
        }
        else
        {
            if (playerStatus.CanAirMove)
            {
                // 空中のジャンプ回数を数える
                playerStatus.IncrementAirMoveCount();

                playerMovement.SwingHammerMoveForward(playerCamera.transform.forward, swingForce, playerStatus.Combo);
                playerAnim.SetAnimationByCameraForward();
                lookDirection = playerMovement.ReturnDirectionForward(cameraLook.rotation);//カメラの向きに合わせた方向を取得
                Debug.Log("空気殴った");
            }
        }

        soundManager.Play(seList["swing"]);

    }

    void UpdatePlayerStatus()
    {
        //壁か敵だったら
        //コンボ数増やす
        playerStatus.Combo++;
        playerStatus.Rate *= 2;

        //リセット
        playerStatus.RechargeAirMove();
    }

    /// <summary>
    /// 入力方向を取得し、反転した値をinputDirectionに格納する
    /// </summary>
    /// <param name="context"></param>
    void OnDirection(InputAction.CallbackContext context)
    {
        //スタン状態なら処理しない
        if (playerStatus.IsStunned)
            return;

        Vector2 newInputDirection = context.ReadValue<Vector2>();

        if (!directionReverse)
            inputDirection = newInputDirection.normalized * -1.0f;
        else
            inputDirection = newInputDirection.normalized;


    }

    /// <summary>
    /// カメラを反転させる
    /// </summary>
    /// <param name="context"></param>
    void ONTurn(InputAction.CallbackContext context)
    {
        playerCamera.TurnCamera();
    }

    /// <summary>
    /// カメラの向きに合わせてプレイヤーを回転させる
    /// </summary>
    void FaceCameraDirection()
    {
        Vector3 cameraForward = cameraLook.forward;
        //Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);//cameraForward→lookDirectionに変更
        transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
    }

    /// <summary>
    /// 一定時間ヒットストップさせる。コンボ数が増えるごとにヒットストップ時間が増える
    /// </summary>
    /// <param name="combo"></param>
    void StartHitStop(int combo)
    {
        float hitStopTime = Mathf.Min(baseHitStopTime  + perComboHitStopTime* combo, maxHitStopTime);

        if (hitStopCoroutine != null)
        {
            StopCoroutine(hitStopCoroutine);
        }
        hitStopCoroutine = StartCoroutine(HitStopCoroutine(hitStopTime));
    }

    IEnumerator HitStopCoroutine(float hitStopTime)
    {
        // ヒットストップ開始
        isHitStop = true;
        playerMovement.HitStopStart();
        playerAnim.HitStopStart();

        yield return new WaitForSeconds(hitStopTime);

        // ヒットストップ終了
        playerMovement.HitStopEnd();
        playerAnim.HitStopEnd();
        isHitStop = false;
        hitStopCoroutine = null;
    }



    void OnTurnBackPressed(InputAction.CallbackContext context)
    {
        playerCamera.BackCamera();
    }

    void OnTurnForwardReleased(InputAction.CallbackContext context)
    {
        if (playerCamera.GetIsTurning())
            playerCamera.StopBackCamera();
    }

    /// <summary>
    /// イベントハンドラの解除
    /// </summary>
    void OnDestroy()
    {
        controls.Player.Direction.performed -= OnDirection;
        controls.Player.Direction.canceled -= OnDirection;
        controls.Player.HammerSwing_LR_UD.performed -= OnHammerSwing;
        controls.Disable();
    }
}
