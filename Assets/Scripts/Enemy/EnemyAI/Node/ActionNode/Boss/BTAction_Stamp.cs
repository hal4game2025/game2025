using UnityEngine;

/// <summary>
/// 3��A���U��
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Action/Stamp")]
public class BTAction_Stamp : BTAction
{
    protected override NodeState NodeUpdate(ref AIController.EnemyData data, in Transform target)
    {
        switch (data.animState)
        {
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

            case AIController.EnemyAnimState.End:
                state = NodeState.Success;
                break;
        }
        
        return state;
    }
}
