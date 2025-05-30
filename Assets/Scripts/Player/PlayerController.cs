using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
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


    EnemyStatus[] enemyList; //�Փ˂����G�̃��X�g

    HammerCollision hammerCollision; //�͂�܁[�̓����蔻��
    PlayerControls controls;         //���̓A�N�V����
    Vector2 inputDirection;          //���͕���
    PlayerStatus playerStatus;       //�v���C���[�̏��
    PlayerAnim playerAnim;           //�v���C���[�̃A�j���[�V����
    Coroutine hitStopCoroutine;      //�q�b�g�X�g�b�v�̂���[����

    bool isHitStop = false;         //�q�b�g�X�g�b�v�����ǂ���

    [SerializeField]
    SoundManager soundManager;
    [SerializeField]
    pair<string,AudioClip>[] playerSE;  // �C���X�y�N�^�[�ݒ�p

    Dictionary<string, AudioClip> seList;

    [SerializeField]
    CameraMovement playerCamera;
    

    //�ǉ�
    EnemyStatus enemyStatus;         //�G�̃X�e�[�^�X
    public EnemyStatus EnemyStatus
    {
        get { return enemyStatus; }
        set { enemyStatus = value; }
    }

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

        seList = new Dictionary<string, AudioClip>();
        foreach (pair<string,AudioClip> temp in playerSE)
        {
            seList.Add(temp.Key, temp.Value);
        }

        //�G�̃��X�g���擾
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

        Debug.Log("rate:" + playerStatus.Rate);
    }

    /// <summary>
    /// �n���}�[�Ŕ�ԁB�ǂ��I��������R���{�����₷
    /// </summary>
    /// <param name="context"></param>
    void OnHammerSwing(InputAction.CallbackContext context)
    {
        //�X�^����Ԃ��A�q�b�g�X�g�b�v��ԂȂ珈�����Ȃ�
        if (playerStatus.IsStunned ||
            isHitStop)
            return;

        //�ǂ��Gor�󒆔���
        if (hammerCollision.IsColliding())
        {

            UpdatePlayerStatus();

            playerMovement.SwingHammer(inputDirection, cameraLook, swingForce, playerStatus.Combo);
            playerAnim.SetAnimationByDirection(inputDirection);

            lookDirection = playerMovement.ReturnDirection(inputDirection, cameraLook.rotation);//�J�����̌����ɍ��킹���������擾



            // ���ʂ���EnemyStatus�����Ƀ_���[�W
            var hammerEnemies = hammerCollision.GetEnemyStatusList();
            if (hammerEnemies != null && enemyList != null)
            {
                for (int i = 0; i < enemyStatusList.Count; i++)
                {
                    SoundManager.Instance.Play(seList["attack"]);
                    enemyStatusList[i].Damage(playerStatus.Rate);
                foreach (var enemy in hammerEnemies)
                {
                    // enemyList�i�z��j�Ɋ܂܂�Ă��邩�`�F�b�N
                    if (System.Array.Exists(enemyList, e => e == enemy))
                    {
                        enemy.Damage(playerStatus.Rate);
                    }
                }
            }

            //�q�b�g�X�g�b�v�J�n
            StartHitStop(playerStatus.Combo);
            Debug.Log("�ǂ��G������");
            soundManager.Play(seList["swing"]);
        }
        else if (playerStatus.CanAirMove)
        {
            // �󒆂̃W�����v�񐔂𐔂���
            playerStatus.IncrementAirMoveCount();

            playerMovement.SwingHammer(inputDirection, cameraLook, swingForce, playerStatus.Combo);
            playerAnim.SetAnimationByDirection(inputDirection);
            lookDirection = playerMovement.ReturnDirection(inputDirection, cameraLook.rotation);//�J�����̌����ɍ��킹���������擾
            Debug.Log("��C������");
            soundManager.Play(seList["swing"]);

        }

        

    }

    void OnHammerSwingMoveForward(InputAction.CallbackContext context)
    {
        //�X�^����Ԃ��A�q�b�g�X�g�b�v��ԂȂ珈�����Ȃ�
        if (playerStatus.IsStunned || isHitStop)
            return;

        //�ǂ��Gor�󒆔���
        if (hammerCollision.IsColliding())
        {
            UpdatePlayerStatus();

            playerMovement.SwingHammerMoveForward(playerCamera.transform.forward,swingForce, playerStatus.Combo);
            playerAnim.SetAnimationByCameraForward();
            lookDirection = playerMovement.ReturnDirectionForward(cameraLook.rotation);//�J�����̌����ɍ��킹���������擾


            // ���ʂ���EnemyStatus�����Ƀ_���[�W
            var hammerEnemies = hammerCollision.GetEnemyStatusList();
            if (hammerEnemies != null && enemyList != null)
            {
                foreach (var enemy in hammerEnemies)
                {
                    SoundManager.Instance.Play(seList["attack"]);
                    enemyStatusList[i].Damage(playerStatus.Rate);
                    // enemyList�i�z��j�Ɋ܂܂�Ă��邩�`�F�b�N
                    if (System.Array.Exists(enemyList, e => e == enemy))
                    {
                        enemy.Damage(playerStatus.Rate);
                    }
                }
            }

            //�q�b�g�X�g�b�v�J�n
            StartHitStop(playerStatus.Combo);
            Debug.Log("�ǂ��G������");
        }
        else
        {
            if (playerStatus.CanAirMove)
            {
                // �󒆂̃W�����v�񐔂𐔂���
                playerStatus.IncrementAirMoveCount();

                playerMovement.SwingHammerMoveForward(playerCamera.transform.forward, swingForce, playerStatus.Combo);
                playerAnim.SetAnimationByCameraForward();
                lookDirection = playerMovement.ReturnDirectionForward(cameraLook.rotation);//�J�����̌����ɍ��킹���������擾
                Debug.Log("��C������");
            }
        }

        soundManager.Play(seList["swing"]);

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
        playerCamera.TurnCamera();
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

    /// <summary>
    /// ��莞�ԃq�b�g�X�g�b�v������B�R���{���������邲�ƂɃq�b�g�X�g�b�v���Ԃ�������
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
        playerCamera.BackCamera();
    }

    void OnTurnForwardReleased(InputAction.CallbackContext context)
    {
        if (playerCamera.GetIsTurning())
            playerCamera.StopBackCamera();
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
