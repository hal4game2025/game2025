using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float swingForce = 10f;//�ς�[
    [SerializeField] Transform cameraLook;
    [SerializeField] float adjustSwingForce = 1.0f;//�R���{���������Ƃ��ɑ����Ȃ�y�[�X

    ChildCollision childCollision;
    PlayerControls controls;
    Vector2 inputDirection;
    IPlayerMovement playerMovement;
    int combo;

    void Start()
    {
        //���ǂ��i�͂�܁[�̓����蔻��j�擾
        childCollision = gameObject.transform.GetChild(0).GetComponent<ChildCollision>();

        controls = new PlayerControls();
        playerMovement = new PlayerMovement(GetComponent<Rigidbody>(), adjustSwingForce);

        // ���̓A�N�V����
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
            Debug.LogError("�R���{�����O�ȉ�����[");
  
        // �J�����̌����ɍ��킹�ăv���C���[����]������
        Vector3 cameraForward = cameraLook.forward;
        cameraForward.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
        transform.rotation = targetRotation;
    }

    /// <summary>
    /// �n���}�[�Ŕ��
    /// </summary>
    /// <param name="context"></param>
    void OnHammerSwing(InputAction.CallbackContext context)
    {
        if (childCollision == null)
        {
            Debug.LogError("ChildCollision�A�^�b�`����ĂȂ�");
            return;
        }


        if (childCollision.IsColliding())
        {
            playerMovement.SwingHammer(inputDirection,swingForce,combo);
            Debug.Log("�ǂ��G������");
        }
        else
        {
            playerMovement.SwingHammer(inputDirection, swingForce, combo);
            Debug.Log("��C������");
        }
    }

    /// <summary>
    /// ���͕������擾���A���]�����l��inputDirection�Ɋi�[����
    /// </summary>
    /// <param name="context"></param>
    void OnDirection(InputAction.CallbackContext context)
    {
        Vector2 newInputDirection = context.ReadValue<Vector2>();
        inputDirection = newInputDirection.normalized * -1;
        Debug.Log(inputDirection);
    }

    /// <summary>
    /// �C�x���g�n���h���̉���
    /// </summary>
    void OnDestroy()
    {
        controls.Player.Direction.performed -= OnDirection;
        controls.Player.Direction.canceled -= OnDirection;
        controls.Player.HammerSwing.performed -= OnHammerSwing;
        controls.Disable();
    }
}
