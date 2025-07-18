using UnityEngine;

/// <summary>
/// �U�艺�낵�U��
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Action/Attack")]
public class BTAction_Attack : BTAction
{
    protected override NodeState NodeUpdate(ref AIController.EnemyData data, in Transform target)
    {
        // �A�j���[�V�������I����Ă��邩�`�F�b�N (���ӓ_�FAnimClip�ŃA�j���[�V�����̍Ō��Event�֐�������)
        if (data.animState == AIController.EnemyAnimState.End)
        {
            state = NodeState.Success;
            return state;
        }

        return state;
    }
}
