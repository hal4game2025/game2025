using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float swingForce = 10f;//ぱわー
    [SerializeField] Transform cameraLook;
    [SerializeField] float adjustSwingForce = 1.0f;//コンボが増えたときに早くなるペース

    ChildCollision childCollision;
    PlayerControls controls;
    Vector2 inputDirection;
    IPlayerMovement playerMovement;
    int combo;

    void Start()
    {
        //こども（はんまーの当たり判定）取得
        childCollision = gameObject.transform.GetChild(0).GetComponent<ChildCollision>();

        controls = new PlayerControls();
        playerMovement = new PlayerMovement(GetComponent<Rigidbody>(), adjustSwingForce);

        // 入力アクション
        controls.Player.Direction.performed += OnDirection;
        controls.Player.Direction.canceled += OnDirection;
        controls.Player.HammerSwing.performed += OnHammerSwing;
        controls.Enable();
        combo = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (combo <= 0)
            Debug.LogError("コンボ数が０以下だよー");
  
        // カメラの向きに合わせてプレイヤーを回転させる
        Vector3 cameraForward = cameraLook.forward;
        cameraForward.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
        transform.rotation = targetRotation;
    }

    /// <summary>
    /// ハンマーで飛ぶ
    /// </summary>
    /// <param name="context"></param>
    void OnHammerSwing(InputAction.CallbackContext context)
    {
        if (childCollision == null)
        {
            Debug.LogError("ChildCollisionアタッチされてない");
            return;
        }


        if (childCollision.IsColliding())
        {
            playerMovement.SwingHammer(inputDirection,swingForce,combo);
            Debug.Log("壁か敵殴った");
        }
        else
        {
            playerMovement.SwingHammer(inputDirection, swingForce, combo);
            Debug.Log("空気殴った");
        }
    }

    /// <summary>
    /// 入力方向を取得し、反転した値をinputDirectionに格納する
    /// </summary>
    /// <param name="context"></param>
    void OnDirection(InputAction.CallbackContext context)
    {
        Vector2 newInputDirection = context.ReadValue<Vector2>();
        inputDirection = newInputDirection.normalized * -1;
        Debug.Log(inputDirection);
    }

    /// <summary>
    /// イベントハンドラの解除
    /// </summary>
    void OnDestroy()
    {
        controls.Player.Direction.performed -= OnDirection;
        controls.Player.Direction.canceled -= OnDirection;
        controls.Player.HammerSwing.performed -= OnHammerSwing;
        controls.Disable();
    }
}
