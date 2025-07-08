using UnityEngine;

public class AIController : MonoBehaviour
{
    /// <summary>
    /// �A�j���[�V�����̏��
    /// </summary>
    public enum EnemyAnimState
    {
        None = 0,   // ��
        End,        // �I��
        Event,      // ����̏��������݂�����
        Start,      // Event�܂ł̏���
        Next,       // ���̃A�j���[�V����������ꍇ
    }

    /// <summary>
    /// �GAI�̎v�l�ɕK�v�ȃf�[�^
    /// </summary>
    [System.Serializable]
    public struct EnemyData
    {
        [HideInInspector] public EnemyStatus status;        // �G�X�e�[�^�X
        [HideInInspector] public EnemyAnimState animState;  // �A�j���[�V�����̏��
        [HideInInspector] public Animator anim;             // �A�j���[�^�[
        public string animParamName;                        // �A�j���[�V�����̃p�����^�[��
        public int animIdel;                                // �ҋ@���[�V�����̐�
        public BoxCollider[] boxColliders;                  // �U���R���C�_�[�܂Ƃ�

        /// <summary>
        /// �A�j���[�V�����̏�Ԃ�ς���
        /// </summary>
        /// <param name="state"></param>
        public void SetEnemyAnimState(EnemyAnimState state)
        {
            animState = state;
        }

        /// <summary>
        /// �U���R���C�_�[��L����
        /// </summary>
        /// <param name="num"></param>
        public void OnBoxCollider(int num)
        {
            boxColliders[num].enabled = true;
        }

        /// <summary>
        /// �U���R���C�_�[�𖳌���
        /// </summary>
        /// <param name="num"></param>
        public void OffBoxCollider(int num)
        {
            boxColliders[num].enabled = false;
        }
    }

    [SerializeField, CustomLabel("�r�w�C�r�A�c���["), Tooltip("ScriptableObject���A�^�b�`")] 
    AIBehaviorTree bt;
    [SerializeField, CustomLabel("�v���C���[�̍��W")]
    Transform playerTransform;

    [CustomLabel("�G�̏��")] public EnemyData data;

    void Start()
    {
        //--- �f�[�^�擾
        data.status = GetComponent<EnemyStatus>();  // �X�e�[�^�X
        data.animState = EnemyAnimState.None;
        data.anim = GetComponent<Animator>();       // �A�j���[�^�[    

        //--- ��O�`�F�b�N
        if (!bt) Debug.Log("AIController AIBehaviorTree���Ȃ�");
        if (!playerTransform) Debug.Log("AIController �v���C���[�̍��W���Ȃ�");
        if (!data.status) Debug.Log("AIController �G�X�e�[�^�X�Ȃ�");
        if (!data.anim) Debug.Log("AIController Animator���Ȃ�");

        // �r�w�C�r�A�c���[�̏�����
        bt.BTInit();
    }

    void Update()
    {
        // BT�X�V
        bt.BTUpdate(data, playerTransform);
    }


    /// <summary>
    /// �A�j���[�V�����C�x���g�Ŏg���p
    /// </summary>
    /// <param name="state"></param>
    public void SetEnemyAnimState(int state)
    {
        data.SetEnemyAnimState((EnemyAnimState)state);
    }

    /// <summary>
    /// �A�j���[�V�����C�x���g�Ŏg���p(�L����)
    /// </summary>
    /// <param name="num"></param>
    public void OnBoxCollider(int num)
    {
        data.OnBoxCollider(num);
    }

    /// <summary>
    /// �A�j���[�V�����C�x���g�Ŏg���p(������)
    /// </summary>
    /// <param name="num"></param>
    public void OffBoxCollider(int num)
    {
        data.OffBoxCollider(num);
    }
}
