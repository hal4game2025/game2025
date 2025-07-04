using UnityEngine;

public class AIController : MonoBehaviour
{
    public enum EnemyAnimState
    {
        None = 0,   // ��
        End,        // �I��
        Event,      // ����̏��������݂�����
    }

    /// <summary>
    /// �GAI�̎v�l�ɕK�v�ȃf�[�^
    /// </summary>
    public struct EnemyData
    {
        [HideInInspector] public EnemyStatus status;    // �G�X�e�[�^�X
        [HideInInspector] public EnemyAnimState state;  // �A�j���[�V�����̏��
        [HideInInspector] public Animator anim;         // �A�j���[�^�[
        // �U���R���C�_�[(�Y�݃|�C���g
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
        data.state = EnemyAnimState.None;
        data.anim = GetComponent<Animator>();       // �A�j���[�^�[    

        //--- ��O�`�F�b�N
        if (!bt) Debug.Log("AIController AIBehaviorTree���Ȃ�");
        if (!playerTransform) Debug.Log("AIController �v���C���[�̍��W���Ȃ�");
        if (!data.status) Debug.Log("AIController �G�X�e�[�^�X�Ȃ�");
        if (!data.anim) Debug.Log("AIController Animator���Ȃ�");

        // �r�w�C�r�A�c���[�̏�����
        bt.BTInit(data, playerTransform);
    }

    void Update()
    {
        // BT�X�V
        bt.BTUpdate();
    }


    /// <summary>
    /// �A�j���[�V�����̏�Ԃ�ς���
    /// </summary>
    /// <param name="state">�A�j���[�V�����C�x���g�ł��g����int�^</param>
    public void SetEnemyAnimState(int state)
    {
        data.state = (EnemyAnimState)state;
    }
}
