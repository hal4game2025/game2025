using UnityEngine;

/// <summary>
/// CD�̊m�F
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Conditions/CheckCD")]
public class BTCheckCD : BTConditions
{
    protected override NodeState NodeUpdate(ref AIController.EnemyData data, in Transform target)
    {
        // CD��0���傫�������玸�s
        return action.currentCD > 0f ? NodeState.Failure : NodeState.Success;
    }
}
