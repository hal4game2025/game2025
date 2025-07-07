using UnityEngine;

/// <summary>
/// �ːi�U��
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Action/Bless")]
public class BTAction_ChargeAttack : BTAction
{
    protected override NodeState NodeUpdate()
    {
        switch (data.animState)
        {
            case AIController.EnemyAnimState.None:
                break;
            case AIController.EnemyAnimState.Start:
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
                // �ːi�U��

                break;
            case AIController.EnemyAnimState.End:
                state = NodeState.Success;
                break;
        }

        return state;
    }
}
