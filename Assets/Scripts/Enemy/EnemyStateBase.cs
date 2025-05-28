using UnityEngine;
using UnityEngine.UIElements;

public class EnemyStateBase : MonoBehaviour
{
    public enum EnemyState
    {
        Idle = 0,   // �s����̍d���Ɏg������
        Chase,      // �v���C���[��ǂ�������̂ƍU���I������
        Action,     // �s������
        Hit,        // ��낯�鏈���Ɏg������
        Max
    }

    [SerializeField, CustomLabel("�v���C���[�̍��W")] Transform player;

    [SerializeField, CustomLabel("�U���R���C�_�[")] protected Collider[] atkCollider;
    protected EnemyStatus status;                       // �X�e�[�^�X
    protected Animator animator;                        // �A�j���[�^�[
    protected EnemyState nowState = EnemyState.Chase;   // ���݂̃X�e�[�g (�ŏ�����ǂ�������
    protected float delay = 0f;     // �s����d������
    protected float distance;       // �v���C���[�Ƃ̋���

    static Vector3 contradictUP = new Vector3(1f, 0f, 1f);  // y���̒l�ł������p

    void Start()
    {
        // �X�e�[�^�X�擾
        status = GetComponent<EnemyStatus>();
        // �A�j���[�^�[�擾
        animator = GetComponent<Animator>();

        // �q�N���X�̏�����
        EnemyStart();
    }

    void Update()
    {
        // �N�[���_�E������
        CD();

        // �v���C���[�Ƃ̋���
        distance = Vector3.Distance(player.position, transform.position);

        switch (nowState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Chase:
                Chase();        // �ǂ�������
                EnemyUpdate();  // �U���I��
                break;
            case EnemyState.Action:
                Action();   // �U������
                break;
            case EnemyState.Hit:
                Hit();
                break;
            default:
                Debug.Log("�G�l�~�[�X�e�[�g��swith�O�Q�Ƃł�");
                break;
        }
    }

    //--- public
    /// <summary>
    /// �X�e�[�g��ύX (�A�j���[�V�����C�x���g�ł��g�p�\��
    /// </summary>
    /// <param name="state"></param>
    public void SetEnemyState(int state)
    {
        // ��O����
        if ((EnemyState)state > EnemyState.Max)
        {
            Debug.Log("EnemyStateBase �񋓊O�Q�Ƃ���܂���");
            return;
        }
        // �X�e�[�g�ύX
        nowState = (EnemyState)state;
        // �A�j���[�V����
        animator.SetInteger("State", (int)nowState);
    }

    /// <summary>
    /// �U���p�R���C�_�[���A�N�e�B�u��
    /// </summary>
    /// <param name="atk"></param>
    public void ActiveCollider(int atk)
    {
        atkCollider[atk].enabled ^= true;
    }


    //--- private
    /// <summary>
    /// �N�[���_�E������
    /// </summary>
    private void CD()
    {
        for (int i = 0; i < status.atkDatas.Length; i++)
        {
            // 0�ȉ��̓X���[
            if (status.atkDatas[i].nowCD <= 0f) continue;
            // �N�[���_�E��
            status.atkDatas[i].nowCD -= Time.deltaTime;
        }
    }

    /// <summary>
    /// �s����d��
    /// </summary>
    private void Idle()
    {
        if (delay <= 0f)
        {
            delay = 0f;
            // �X�e�[�g��Chase�ɕύX
            SetEnemyState((int)EnemyState.Chase);
            return;
        }

        // �f�B���C����
        delay -= Time.deltaTime;
        animator.SetInteger("Action", -1);
    }

    /// <summary>
    /// �ǂ������鏈��
    /// </summary>
    private void Chase()
    {
        // �v���C���[�̕�������
        Vector3 pos = player.position - transform.position;                      // �����x�N�g��
        Quaternion rot = Quaternion.LookRotation(Vector3.Scale(pos, contradictUP)); // ��������]�ɕϊ�
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, status.LookSpeed * Time.deltaTime);  // �K�p

        // �ő�ڋߋ�������Ȃ�������ǂ�������
        if (distance < status.Distance) return;

        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(player.position.x, transform.position.y, player.position.z),  // y���͌��̂܂�
            status.MoveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// ��낯����
    /// </summary>
    private void Hit()
    {

    }


    //--- �q�N���X
    /// <summary>
    /// �q�N���X�̏�����
    /// </summary>
    protected virtual void EnemyStart() { }
    /// <summary>
    /// �U���I��
    /// </summary>
    protected virtual void EnemyUpdate() { }
    /// <summary>
    /// �U������
    /// </summary>
    protected virtual void Action() { }
}
