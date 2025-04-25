using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField][Tooltip("ベースのパワー")] float swingForce = 10f;//ぱわー
    [SerializeField] Transform cameraLook;
    [SerializeField][Tooltip("コンボが増えたときに早くなるペース")] float adjustSwingForce = 1.0f;//コンボが増えたときに早くなるペース
    [SerializeField] bool directionReverse = false;// trueなら入力方向を反転
    [SerializeField] bool processOnlyOnCollision = false; // trueなら壁か敵に当たったときだけ
    [SerializeField] int max_speed_coef = 1;

    //[SerializeField] Text text;      //コンボ数表示（デバッグ用）

    HammerCollision hammerCollision; //はんまーの当たり判定
    PlayerControls controls;         //入力アクション
    Vector2 inputDirection;          //入力方向
    [SerializeField]  PlayerMovement playerMovement;  //プレイヤーの動き
    PlayerStatus playerStatus;       //プレイヤーの状態
    PlayerAnim playerAnim;           //プレイヤーのアニメーション

     Vector3 lookDirection;
    void Start()
    {
       
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

    }

    /// <summary>
    /// ハンマーで飛ぶ。壁か的だったらコンボ数増やす
    /// </summary>
    /// <param name="context"></param>
    void OnHammerSwing(InputAction.CallbackContext context)
    {
        //スタン状態なら処理しない
        if (playerStatus.IsStunned)
            return;

        //壁か敵or空中判定
        if (hammerCollision.IsColliding())
        {
            //壁か敵だったら
            //コンボ数増やす
            playerStatus.Combo++;
            //リセット
            playerStatus.ResetAirJumpCount();
            playerStatus.ResetAirMoveTimer();

            playerMovement.SwingHammer(inputDirection, cameraLook.rotation, swingForce, playerStatus.Combo);
            playerAnim.SetAnimationByDirection(inputDirection);

            lookDirection = playerMovement.ReturnDirection(inputDirection,cameraLook.rotation);//カメラの向きに合わせた方向を取得
            Debug.Log("壁か敵殴った");
        }
        else
        {
            if (playerStatus.CanAirMove)
            {
                // 空中のジャンプ回数を数える
                playerStatus.IncrementAirJumpCount();

                playerMovement.SwingHammer(inputDirection, cameraLook.rotation, swingForce, playerStatus.Combo);
                playerAnim.SetAnimationByDirection(inputDirection);
                lookDirection = playerMovement.ReturnDirection(inputDirection, cameraLook.rotation);//カメラの向きに合わせた方向を取得
                Debug.Log("空気殴った");
            }
        }
        
    }


    void OnHammerSwingMoveForward(InputAction.CallbackContext context)
    {
        //スタン状態なら処理しない
        if (playerStatus.IsStunned)
            return;

        //壁か敵or空中判定
        if (hammerCollision.IsColliding())
        {
            //壁か敵だったら
            //コンボ数増やす
            playerStatus.Combo++;
            //カウントリセット
            playerStatus.ResetAirJumpCount();
            playerStatus.ResetAirMoveTimer();

            playerMovement.SwingHammerMoveForward(CameraMovement.instance.transform.forward,swingForce, playerStatus.Combo);
            playerAnim.SetAnimationByCameraForward();
            lookDirection = playerMovement.ReturnDirectionForward(cameraLook.rotation);//カメラの向きに合わせた方向を取得
            Debug.Log("壁か敵殴った");
        }
        else
        {
            if (playerStatus.CanAirMove)
            {
                // 空中のジャンプ回数を数える
                playerStatus.IncrementAirJumpCount();

                playerMovement.SwingHammerMoveForward(CameraMovement.instance.transform.forward, swingForce, playerStatus.Combo);
                playerAnim.SetAnimationByCameraForward();
                lookDirection = playerMovement.ReturnDirectionForward(cameraLook.rotation);//カメラの向きに合わせた方向を取得
                Debug.Log("空気殴った");
            }
        }
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
        CameraMovement.instance.TurnCamera();
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

    void OnTurnBackPressed(InputAction.CallbackContext context)
    {
        CameraMovement.instance.BackCamera();
    }

    void OnTurnForwardReleased(InputAction.CallbackContext context)
    {
        if (CameraMovement.instance.GetIsTurning())
            CameraMovement.instance.StopBackCamera();
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
