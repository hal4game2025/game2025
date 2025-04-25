using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField][Tooltip("�x�[�X�̃p���[")] float swingForce = 10f;//�ς�[
    [SerializeField] Transform cameraLook;
    [SerializeField][Tooltip("�R���{���������Ƃ��ɑ����Ȃ�y�[�X")] float adjustSwingForce = 1.0f;//�R���{���������Ƃ��ɑ����Ȃ�y�[�X
    [SerializeField] bool directionReverse = false;// true�Ȃ���͕����𔽓]
    [SerializeField] bool processOnlyOnCollision = false; // true�Ȃ�ǂ��G�ɓ��������Ƃ�����
    [SerializeField] int max_speed_coef = 1;

    //[SerializeField] Text text;      //�R���{���\���i�f�o�b�O�p�j

    HammerCollision hammerCollision; //�͂�܁[�̓����蔻��
    PlayerControls controls;         //���̓A�N�V����
    Vector2 inputDirection;          //���͕���
    [SerializeField]  PlayerMovement playerMovement;  //�v���C���[�̓���
    PlayerStatus playerStatus;       //�v���C���[�̏��
    PlayerAnim playerAnim;           //�v���C���[�̃A�j���[�V����

     Vector3 lookDirection;
    void Start()
    {
       
        hammerCollision = gameObject.transform.GetChild(0).GetComponent<HammerCollision>(); //���ǂ��i�͂�܁[�̓����蔻��j�擾
        playerStatus = GetComponent<PlayerStatus>();
        playerAnim = GetComponent<PlayerAnim>();
        controls = new PlayerControls();
        playerMovement = new PlayerMovement(GetComponent<Rigidbody>(), adjustSwingForce);
        playerMovement.max_coef = max_speed_coef;

        // ���̓A�N�V����
        controls.Player.Direction.performed += OnDirection;
        controls.Player.Direction.canceled += OnDirection;
        controls.Player.HammerSwing_LR_UD.performed += OnHammerSwing;
        controls.Player.Trun.performed += ONTurn;
        controls.Player.HammerSwingMoveForward.performed += OnHammerSwingMoveForward;
        controls.Enable();

        
    }

    void Update()
    {
        //�J�����̌����ɍ��킹�ăv���C���[����]������
        FaceCameraDirection();

        //�f�o�b�O�p
        //text.text = "�R���{��:" + playerStatus.Combo;

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

        //�ǂ��Gor�󒆔���
        if (hammerCollision.IsColliding())
        {
            //�ǂ��G��������
            //�R���{�����₷
            playerStatus.Combo++;
            //���Z�b�g
            playerStatus.ResetAirJumpCount();
            playerStatus.ResetAirMoveTimer();

            playerMovement.SwingHammer(inputDirection, cameraLook.rotation, swingForce, playerStatus.Combo);
            playerAnim.SetAnimationByDirection(inputDirection);

            lookDirection = playerMovement.ReturnDirection(inputDirection,cameraLook.rotation);//�J�����̌����ɍ��킹���������擾
            Debug.Log("�ǂ��G������");
        }
        else
        {
            if (playerStatus.CanAirMove)
            {
                // �󒆂̃W�����v�񐔂𐔂���
                playerStatus.IncrementAirJumpCount();

                playerMovement.SwingHammer(inputDirection, cameraLook.rotation, swingForce, playerStatus.Combo);
                playerAnim.SetAnimationByDirection(inputDirection);
                lookDirection = playerMovement.ReturnDirection(inputDirection, cameraLook.rotation);//�J�����̌����ɍ��킹���������擾
                Debug.Log("��C������");
            }
        }
        
    }


    void OnHammerSwingMoveForward(InputAction.CallbackContext context)
    {
        //�X�^����ԂȂ珈�����Ȃ�
        if (playerStatus.IsStunned)
            return;

        //�ǂ��Gor�󒆔���
        if (hammerCollision.IsColliding())
        {
            //�ǂ��G��������
            //�R���{�����₷
            playerStatus.Combo++;
            //�J�E���g���Z�b�g
            playerStatus.ResetAirJumpCount();
            playerStatus.ResetAirMoveTimer();

            playerMovement.SwingHammerMoveForward(CameraMovement.instance.transform.forward,swingForce, playerStatus.Combo);
            playerAnim.SetAnimationByCameraForward();
            lookDirection = playerMovement.ReturnDirectionForward(cameraLook.rotation);//�J�����̌����ɍ��킹���������擾
            Debug.Log("�ǂ��G������");
        }
        else
        {
            if (playerStatus.CanAirMove)
            {
                // �󒆂̃W�����v�񐔂𐔂���
                playerStatus.IncrementAirJumpCount();

                playerMovement.SwingHammerMoveForward(CameraMovement.instance.transform.forward, swingForce, playerStatus.Combo);
                playerAnim.SetAnimationByCameraForward();
                lookDirection = playerMovement.ReturnDirectionForward(cameraLook.rotation);//�J�����̌����ɍ��킹���������擾
                Debug.Log("��C������");
            }
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

    /// <summary>
    /// �J�����̌����ɍ��킹�ăv���C���[����]������
    /// </summary>
    void FaceCameraDirection()
    {
        Vector3 cameraForward = cameraLook.forward;
        //Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);//cameraForward��lookDirection�ɕύX
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
