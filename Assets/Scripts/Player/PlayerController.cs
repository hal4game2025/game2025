
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using MySystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField][Tooltip("�x�[�X�̃p���[")] float swingForce = 10f;//�ς�[
    [SerializeField] Transform cameraLook;
    [SerializeField][Tooltip("�R���{���������Ƃ��ɑ����Ȃ�y�[�X")] float adjustSwingForce = 1.0f;//�R���{���������Ƃ��ɑ����Ȃ�y�[�X
    [SerializeField] bool directionReverse = false;// true�Ȃ���͕����𔽓]
    [SerializeField] bool processOnlyOnCollision = false; // true�Ȃ�ǂ��G�ɓ��������Ƃ�����
    [SerializeField] int max_speed_coef = 1;
    [SerializeField] PlayerMovement playerMovement;  //�v���C���[�̓���


    [CustomLabel("�ŏ��q�b�g�X�g�b�v����")]
    [SerializeField] float baseHitStopTime = 0.1f; //�R���{����0�̎��̃x�[�X�q�b�g�X�g�b�v����
    [CustomLabel("�ő�q�b�g�X�g�b�v����")]
    [SerializeField] float maxHitStopTime = 0.5f; //�ő�q�b�g�X�g�b�v����
    [CustomLabel("�q�b�g�X�g�b�v�̃R���{�{��")]
    [SerializeField] float perComboHitStopTime = 0.05f; //�R���{���ɉ����A���񂾂�q�b�g�X�g�b�v���Ԃ�������


    HammerCollision hammerCollision; //�͂�܁[�̓����蔻��
    PlayerControls controls;         //���̓A�N�V����
    Vector2 inputDirection;          //���͕���
    PlayerStatus playerStatus;       //�v���C���[�̏��
    PlayerAnim playerAnim;           //�v���C���[�̃A�j���[�V����
    Coroutine hitStopCoroutine;      //�q�b�g�X�g�b�v�̂���[����
    [SerializeField]
    CameraMovement cameraMovement;


    bool isHitStop = false;         //�q�b�g�X�g�b�v�����ǂ���

    [SerializeField]
    pair<string, AudioClip>[] playerSE;
    Dictionary<string, AudioClip> playerSEDict = new Dictionary<string, AudioClip>(); //�v���C���[��SE���i�[���鎫��



    //��
    Vector3 lookDirection;
    void Start()
    {
        // �}�E�X�Œ聕��\��
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

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

        foreach(pair<string,AudioClip> se in playerSE)
        {
            playerSEDict.Add(se.Key, se.Value);
        }

    }

    void Update()
    {

    }

    /// <summary>
    /// �n���}�[�Ŕ�ԁB�ǂ��I��������R���{�����₷
    /// </summary>
    /// <param name="context"></param>
    void OnHammerSwing(InputAction.CallbackContext context)
    {
        if (gameObject.activeInHierarchy == false)
            return;

        //�X�^����Ԃ��A�q�b�g�X�g�b�v��ԂȂ珈�����Ȃ�
        if (playerStatus.IsStunned ||
            isHitStop)
            return;
        CollisionType hitCollisionType = hammerCollision.GetCollidingType();

        switch (hitCollisionType)
        {
            case CollisionType.Enemy:
                UpdatePlayerStatus();
                playerMovement.SwingHammer(inputDirection, cameraLook, swingForce, playerStatus.Combo);
                SwingAction();
                EnemyDamage();
                StartHitStop(playerStatus.Combo);
                SoundManager.Instance.Play(playerSEDict["attack"]);
                break;
            case CollisionType.Obstacles:
                UpdatePlayerStatus();
                playerMovement.SwingHammer(inputDirection, cameraLook, swingForce, playerStatus.Combo);
                SoundManager.Instance.Play(playerSEDict["swing"]);
                SwingAction();
           
                break;
            case CollisionType.None:

                if (playerStatus.CanAirMove)
                {
                    // �󒆂̃W�����v�񐔂𐔂���
                    playerStatus.IncrementAirMoveCount();
                    playerMovement.SwingHammer(inputDirection, cameraLook, swingForce, playerStatus.Combo);
                    SoundManager.Instance.Play(playerSEDict["swing"]);
                    SwingAction();
                }
                break;

        }
        playerAnim.SetAnimationByDirection(inputDirection);
    }

    void OnHammerSwingMoveForward(InputAction.CallbackContext context)
    {
        if(gameObject.activeInHierarchy == false)
            return;

        //�X�^����Ԃ��A�q�b�g�X�g�b�v��ԂȂ珈�����Ȃ�
        if (playerStatus.IsStunned ||
            isHitStop)
            return;

        CollisionType hitCollisionType = hammerCollision.GetCollidingType();
        switch (hitCollisionType)
        {
            case CollisionType.Enemy:
                UpdatePlayerStatus();

                playerMovement.SwingHammerMoveForward(cameraMovement.transform.forward, swingForce, playerStatus.Combo);
                SwingActionForward();
                EnemyDamage();
                SoundManager.Instance.Play(playerSEDict["attack"]);
                StartHitStop(playerStatus.Combo);
                break;
            case CollisionType.Obstacles:
                UpdatePlayerStatus();
                playerMovement.SwingHammerMoveForward(cameraMovement.transform.forward, swingForce, playerStatus.Combo);
                SoundManager.Instance.Play(playerSEDict["swing"]);
                SwingActionForward();
                break;
            case CollisionType.None:

                if (playerStatus.CanAirMove)
                {
                    // �󒆂̃W�����v�񐔂𐔂���
                    playerStatus.IncrementAirMoveCount();
                    playerMovement.SwingHammerMoveForward(cameraMovement.transform.forward, swingForce, playerStatus.Combo);
                    SoundManager.Instance.Play(playerSEDict["swing"]);
                    SwingActionForward();
                }
                break;

        }
    }

    void EnemyDamage()
    {

        List<EnemyStatus> enemyStatusList = hammerCollision.GetEnemyStatusList();
        if (enemyStatusList != null)
        {
            for (int i = 0; i < enemyStatusList.Count; i++)
            {
                enemyStatusList[i].Damage(playerStatus.Rate);
            }
        }
    }

    void SwingAction()
    {
        playerAnim.SetAnimationByDirection(inputDirection);
        Vector3 cameraForward = cameraLook.forward;
        Quaternion targetRotation = Quaternion.LookRotation(cameraLook.forward);
        transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
    }

    void SwingActionForward()
    {
        playerAnim.SetAnimationByCameraForward();
        Quaternion targetRotation = Quaternion.LookRotation(cameraLook.forward);
        transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
    }

    void SwingActionAir()
    {
        if (playerStatus.CanAirMove)
        {
            // �󒆂̃W�����v�񐔂𐔂���
            playerStatus.IncrementAirMoveCount();

            playerMovement.SwingHammer(inputDirection, cameraLook, swingForce, playerStatus.Combo);
            playerAnim.SetAnimationByDirection(inputDirection);
            lookDirection = playerMovement.ReturnDirection(inputDirection, cameraLook.rotation);//�J�����̌����ɍ��킹���������擾
            Debug.Log("��C������");
        }
    }


    void UpdatePlayerStatus()
    {
        //�ǂ��G��������
        //�R���{�����₷
        playerStatus.Combo++;
        playerStatus.Rate *= 2;

        //���Z�b�g
        playerStatus.RechargeAirMove();
    }

    /// <summary>
    /// ���͕������擾���A���]�����l��inputDirection�Ɋi�[����
    /// </summary>
    /// <param name="context"></param>
    void OnDirection(InputAction.CallbackContext context)
    {
        if (gameObject.activeInHierarchy == false)
            return;

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
        cameraMovement.TurnCamera();
    }

    /// <summary>
    /// �J�����̌����ɍ��킹�ăv���C���[����]������
    /// </summary>
    void FaceCameraDirection()
    {
        Vector3 cameraForward = cameraLook.forward;
        Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
        transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
    }

    /// <summary>
    /// ��莞�ԃq�b�g�X�g�b�v������B�R���{���������邲�ƂɃq�b�g�X�g�b�v���Ԃ�������
    /// </summary>
    /// <param name="combo"></param>
    void StartHitStop(int combo)
    {
        float hitStopTime = Mathf.Min(baseHitStopTime + perComboHitStopTime * combo, maxHitStopTime);

        if (hitStopCoroutine != null)
        {
            StopCoroutine(hitStopCoroutine);
        }
        hitStopCoroutine = StartCoroutine(HitStopCoroutine(hitStopTime));
    }

    IEnumerator HitStopCoroutine(float hitStopTime)
    {
        // �q�b�g�X�g�b�v�J�n
        isHitStop = true;
        playerMovement.HitStopStart();
        playerAnim.HitStopStart();

        yield return new WaitForSeconds(hitStopTime);

        // �q�b�g�X�g�b�v�I��
        playerMovement.HitStopEnd();
        playerAnim.HitStopEnd();
        isHitStop = false;
        hitStopCoroutine = null;
    }



    void OnTurnBackPressed(InputAction.CallbackContext context)
    {
        cameraMovement.BackCamera();
    }

    void OnTurnForwardReleased(InputAction.CallbackContext context)
    {
        if (cameraMovement.GetIsTurning())
            cameraMovement.StopBackCamera();
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
