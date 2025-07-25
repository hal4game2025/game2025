using UnityEngine;

/// <summary>
/// CDの確認
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Conditions/CheckCD")]
public class BTCheckCD : BTConditions
{
    protected override NodeState NodeUpdate(ref AIController.EnemyData data, in Transform target)
    {
        // CDが0より大きかったら失敗
        return action.currentCD > 0f ? NodeState.Failure : NodeState.Success;
    }
}
