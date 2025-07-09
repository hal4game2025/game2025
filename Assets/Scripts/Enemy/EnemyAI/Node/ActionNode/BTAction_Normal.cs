using UnityEngine;

/// <summary>
/// �A�j���[�V�������Đ����邾��
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Action/Normal")]
public class BTAction_Normal : BTAction
{
    protected override NodeState NodeUpdate()
    {
        if (data.animState == AIController.EnemyAnimState.End)
        {
            state = NodeState.Success;
        }

        return state;
    }
}
