using UnityEngine;

/// <summary>
/// CD‚ÌŠm”F
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Conditions/CheckCD")]
public class BTCheckCD : BTConditions
{
    protected override NodeState NodeUpdate(ref AIController.EnemyData data, in Transform target)
    {
        // CD‚ª0‚æ‚è‘å‚«‚©‚Á‚½‚çŽ¸”s
        return action.currentCD > 0f ? NodeState.Failure : NodeState.Success;
    }
}
