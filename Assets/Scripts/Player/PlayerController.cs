using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float swingForce = 10f;//�ς�[
    [SerializeField] Transform cameraLook;
    [SerializeField] float adjustSwingForce = 1.0f;//�R���{���������Ƃ��ɑ����Ȃ�y�[�X
    [SerializeField] bool directionReverse = false;// true�Ȃ���͕����𔽓]
    [SerializeField] bool processOnlyOnCollision = false; // true�Ȃ�ǂ��G�ɓ��������Ƃ�����

    [SerializeField] Text text;      //�R���{���\���i�f�o�b�O�p�j

    HammerCollision hammerCollision; //�͂�܁[�̓����蔻��
    PlayerControls controls;         //���̓A�N�V����
    Vector2 inputDirection;          //���͕���
    IPlayerMovement playerMovement;  //�v���C���[�̓���
    PlayerStatus playerStatus;       //�v���C���[�̏��
    PlayerAnim playerAnim;           //�v���C���[�̃A�j���[�V����

    void Start()
    {
       
        hammerCollision = gameObject.transform.GetChild(0).GetComponent<HammerCollision>(); //���ǂ��i�͂�܁[�̓����蔻��j�擾
        playerStatus = GetComponent<PlayerStatus>();
        playerAnim = GetComponent<PlayerAnim>();
        controls = new PlayerControls();
        playerMovement = new PlayerMovement(GetComponent<Rigidbody>(), adjustSwingForce);

        // ���̓A�N�V����
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
        //���������Ă��Ȃ��Ƃ�����
        if (!CameraMovement.instance.GetIsTurning())
        {
            // �J�����̌����ɍ��킹�ăv���C���[����]������
            Vector3 cameraForward = cameraLook.forward;
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
        }
      

        text.text = "�R���{��:" + playerStatus.Combo;


        //�f�o�b�O�p�R���{������
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
    /// �n���}�[�Ŕ�ԁB�ǂ��I��������R���{�����₷
    /// </summary>
    /// <param name="context"></param>
    void OnHammerSwing(InputAction.CallbackContext context)
    {
        //�X�^����ԂȂ珈�����Ȃ�
        if (playerStatus.IsStunned)
            return;

        //�ǂ��G��������R���{�����₷
        if (hammerCollision.IsColliding())
        {
            playerStatus.Combo++;
            playerMovement.SwingHammer(inputDirection, cameraLook.rotation, swingForce, playerStatus.Combo);
            playerAnim.SetAnimationByDirection(inputDirection);
            Debug.Log("�ǂ��G������");
        }
        else
        {
            if (!processOnlyOnCollision)
                playerMovement.SwingHammer(inputDirection, cameraLook.rotation, swingForce, playerStatus.Combo);
            playerAnim.SetAnimationByDirection(inputDirection);
            Debug.Log("��C������");
        }
    }

    
    void OnHammerSwingMoveForward(InputAction.CallbackContext context)
    {
        //�X�^����ԂȂ珈�����Ȃ�
        if (playerStatus.IsStunned)
            return;
        //�ǂ��G��������R���{�����₷
        if (hammerCollision.IsColliding())
        {
            playerStatus.Combo++;
            playerMovement.SwingHammerMoveForward(CameraMovement.instance.transform.forward,swingForce, playerStatus.Combo);
            playerAnim.SetAnimationByCameraForward();
            Debug.Log("�ǂ��G������");
        }
        else
        {
            if (!processOnlyOnCollision)
                playerMovement.SwingHammerMoveForward(CameraMovement.instance.transform.forward,swingForce, playerStatus.Combo);
            playerAnim.SetAnimationByCameraForward();
            Debug.Log("��C������");
        }
    }

    /// <summary>
    /// ���͕������擾���A���]�����l��inputDirection�Ɋi�[����
    /// </summary>
    /// <param name="context"></param>
    void OnDirection(InputAction.CallbackContext context)
    {
        //�X�^����ԂȂ珈�����Ȃ�
        if (playerStatus.IsStunned)
            return;

        Vector2 newInputDirection = context.ReadValue<Vector2>();

        if (!directionReverse)
            inputDirection = newInputDirection.normalized * -1.0f;
        else
            inputDirection = newInputDirection.normalized;


    }

    /// <summary>
    /// �J�����𔽓]������
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
    /// �C�x���g�n���h���̉���
    /// </summary>
    void OnDestroy()
    {
        controls.Player.Direction.performed -= OnDirection;
        controls.Player.Direction.canceled -= OnDirection;
        controls.Player.HammerSwing_LR_UD.performed -= OnHammerSwing;
        controls.Disable();
    }
}
