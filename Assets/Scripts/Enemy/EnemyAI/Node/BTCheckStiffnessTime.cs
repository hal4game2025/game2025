using UnityEngine;

/// <summary>
/// 硬直時間をチェック
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Conditions/CheckStiffnessTime")]
public class BTCheckStiffnessTime : BTConditions
{
    protected override NodeState NodeUpdate(ref AIController.EnemyData data, in Transform target)
    {
        if (data.status.stiffnessTime > 0f) data.status.stiffnessTime -= Time.deltaTime;

        // 硬直時間が0より大きかったら失敗
        return data.status.stiffnessTime > 0f ? NodeState.Failure : NodeState.Success;
    }
}
