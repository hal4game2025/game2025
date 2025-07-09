using UnityEngine;

/// <summary>
/// d’¼ŠÔ‚ğƒ`ƒFƒbƒN
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Conditions/CheckStiffnessTime")]
public class BTCheckStiffnessTime : BTConditions
{
    protected override NodeState NodeUpdate()
    {
        if (data.status.stiffnessTime > 0f) data.status.stiffnessTime -= Time.deltaTime;

        // d’¼ŠÔ‚ª0‚æ‚è‘å‚«‚©‚Á‚½‚ç¸”s
        return data.status.stiffnessTime > 0f ? NodeState.Failure : NodeState.Success;
    }
}
