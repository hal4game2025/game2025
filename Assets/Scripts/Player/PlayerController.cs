using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float swingForce = 10f;//ぱわー
    [SerializeField] Transform cameraLook;
    [SerializeField] float adjustSwingForce = 1.0f;//コンボが増えたときに早くなるペース
    [SerializeField] bool directionReverse = false;// trueなら入力方向を反転
    [SerializeField] bool processOnlyOnCollision = false; // trueなら壁か敵に当たったときだけ

    [SerializeField] Text text;      //コンボ数表示（デバッグ用）

    HammerCollision hammerCollision; //はんまーの当たり判定
    PlayerControls controls;         //入力アクション
    Vector2 inputDirection;          //入力方向
    IPlayerMovement playerMovement;  //プレイヤーの動き
    PlayerStatus playerStatus;       //プレイヤーの状態
    PlayerAnim playerAnim;           //プレイヤーのアニメーション

    void Start()
    {
       
        hammerCollision = gameObject.transform.GetChild(0).GetComponent<HammerCollision>(); //こども（はんまーの当たり判定）取得
        playerStatus = GetComponent<PlayerStatus>();
        playerAnim = GetComponent<PlayerAnim>();
        controls = new PlayerControls();
        playerMovement = new PlayerMovement(GetComponent<Rigidbody>(), adjustSwingForce);

        // 入力アクション
        controls.Player.Direction.performed += OnDirection;
        controls.Player.Direction.canceled += OnDirection;
        controls.Player.HammerSwing_LR_UD.performed += OnHammerSwing;
       controls.Player.TrunForward.performed += OnTurnForwardReleased;
       controls.Player.TrunBack.performed += OnTurnBackPressed;
        controls.Player.HammerSwingMoveForward.performed += OnHammerSwingMoveForward;
        controls.Enable();
    }

    void Update()
    {
        //後ろを向いていないときだけ
        if (!CameraMovement.instance.GetIsTurning())
        {
            // カメラの向きに合わせてプレイヤーを回転させる
            Vector3 cameraForward = cameraLook.forward;
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
        }
      

        text.text = "コンボ数:" + playerStatus.Combo;


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

        //壁か敵だったらコンボ数増やす
        if (hammerCollision.IsColliding())
        {
            playerStatus.Combo++;
            playerMovement.SwingHammer(inputDirection, cameraLook.rotation, swingForce, playerStatus.Combo);
            playerAnim.SetAnimationByDirection(inputDirection);
            Debug.Log("壁か敵殴った");
        }
        else
        {
            if (!processOnlyOnCollision)
                playerMovement.SwingHammer(inputDirection, cameraLook.rotation, swingForce, playerStatus.Combo);
            playerAnim.SetAnimationByDirection(inputDirection);
            Debug.Log("空気殴った");
        }
    }

    
    void OnHammerSwingMoveForward(InputAction.CallbackContext context)
    {
        //スタン状態なら処理しない
        if (playerStatus.IsStunned)
            return;
        //壁か敵だったらコンボ数増やす
        if (hammerCollision.IsColliding())
        {
            playerStatus.Combo++;
            playerMovement.SwingHammerMoveForward(CameraMovement.instance.transform.forward,swingForce, playerStatus.Combo);
            playerAnim.SetAnimationByCameraForward();
            Debug.Log("壁か敵殴った");
        }
        else
        {
            if (!processOnlyOnCollision)
                playerMovement.SwingHammerMoveForward(CameraMovement.instance.transform.forward,swingForce, playerStatus.Combo);
            playerAnim.SetAnimationByCameraForward();
            Debug.Log("空気殴った");
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
