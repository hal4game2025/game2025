using UnityEngine;
using UnityEngine.UIElements;

public class EnemyStateBase : MonoBehaviour
{
    public enum EnemyState
    {
        Idle = 0,   // �U����̍d���Ɏg������
        Chase,      // �v���C���[��ǂ�������̂ƍU���I������
        Attack,     // �U������
        Hit,        // ��낯�鏈���Ɏg������
        Max
    }

    [SerializeField, CustomLabel("�v���C���[�̍��W")] Transform playerPos;

    protected EnemyStatus status;                       // �X�e�[�^�X
    protected EnemyState nowState = EnemyState.Chase;   // ���݂̃X�e�[�g (�ŏ�����ǂ�������
    protected float playerDistance;                     // �v���C���[�Ƃ̋���

    static Vector3 contradictUP = new Vector3(1f, 0f, 1f);  // y���̒l�ł������p

    void Start()
    {
        // �X�e�[�^�X�擾
        status = GetComponent<EnemyStatus>();
        // �v���C���[�Ƃ̋���
        playerDistance = Vector3.Distance(playerPos.position, transform.position);

        // �q�N���X�̏�����
        EnemyStart();
    }

    void Update()
    {
        // �N�[���_�E������
        CD();

        // �v���C���[�Ƃ̋���
        playerDistance = Vector3.Distance(playerPos.position, transform.position);

        switch (nowState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Chase:
                Chase();        // �ǂ�������
                EnemyUpdate();  // �U���I��

                break;
            case EnemyState.Attack:
                Attack();   // �U������

                break;
            case EnemyState.Hit:
                break;
            default:
                Debug.Log("�G�l�~�[�X�e�[�g��swith�O�Q�Ƃł�");
                break;
        }
    }


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
    }


    //--- private
    /// <summary>
    /// �N�[���_�E������
    /// </summary>
    private void CD()
    {
        for (int i = 0; i < status.atkData.Length; i++)
        {
            // 0�ȉ��̓X���[
            if (status.atkData[i].nowCD <= 0f) continue;
            // �N�[���_�E��
            status.atkData[i].nowCD -= Time.deltaTime;
        }
    }

    /// <summary>
    /// �ǂ������鏈��
    /// </summary>
    private void Chase()
    {
        // �v���C���[�̕�������
        Vector3 pos = playerPos.position - transform.position;                      // �����x�N�g��
        Quaternion rot = Quaternion.LookRotation(Vector3.Scale(pos, contradictUP)); // ��������]�ɕϊ�
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, status.LookSpeed * Time.deltaTime);  // �K�p

        // �ő�ڋߋ�������Ȃ�������ǂ�������
        if (playerDistance < status.Distance) return;

        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(playerPos.position.x, transform.position.y, playerPos.position.z),  // y���͌��̂܂�
            status.MoveSpeed * Time.deltaTime);
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
    protected virtual void Attack() { }
}
