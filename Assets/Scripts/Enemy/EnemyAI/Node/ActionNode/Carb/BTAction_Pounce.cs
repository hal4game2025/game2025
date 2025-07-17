using UnityEngine;

/// <summary>
/// ��т��U��
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Action/Pounce")]
public class BTAction_Pounce : BTAction
{
    [SerializeField, Tooltip("�ړ����x")] float moveSpeed = 1.0f;
    Vector3 attackPos;    // �U�����ɎQ�Ƃ�����W (�ǔ��ł͂Ȃ��A�U�����̍��W�֔�т�)

    protected override NodeState NodeUpdate(ref AIController.EnemyData data, in Transform target)
    {
        switch (data.animState)
        {
            case AIController.EnemyAnimState.None:
                break;
            case AIController.EnemyAnimState.Start:
                // �U�����ɎQ�Ƃ���^�[�Q�b�g�̍��W���L��
                attackPos = target.position;
                //--- �^�[�Q�b�g�̕�������
                Transform transform = data.status.transform;
                Vector3 pos = target.position - transform.position; // �^�[�Q�b�g����G�l�~�[�܂ł̃x�N�g��
                pos.y = 0f;
                Quaternion look = Quaternion.LookRotation(pos);     // ��]�ɕϊ�
                // �K�p
                data.status.transform.rotation =
                    Quaternion.Slerp(transform.rotation, look, data.status.LookSpeed);

                break;
            case AIController.EnemyAnimState.Event:
                // �^�[�Q�b�g�̍��W�ֈړ����U�� (�U���R���C�_�[�̓A�j���[�V�����C�x���g�ŗL����)
                data.status.transform.position = Vector3.MoveTowards(
                    data.status.transform.position,
                    new Vector3(attackPos.x, data.status.transform.position.y, attackPos.z),
                    moveSpeed * Time.deltaTime);
                
                break;
            case AIController.EnemyAnimState.End:
                state = NodeState.Success;
                break;
        }

        return state;
    }
}
