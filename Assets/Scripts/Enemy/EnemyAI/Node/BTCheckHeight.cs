using UnityEngine;

/// <summary>
/// ターゲットの高さを確認する
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Conditions/CheckHeight")]
public class BTCheckHeight : BTConditions
{
    [SerializeField, Tooltip("最大高さ")] float maxHeight = 1f;
    [SerializeField, Tooltip("最小高さ")] float minHeight = 0f;

    protected override NodeState NodeUpdate(ref AIController.EnemyData data, in Transform target)
    {
        Vector3 pos = data.status.transform.position;

        return pos.y + maxHeight >= target.position.y && target.position.y > pos.y + minHeight ?
               NodeState.Success : NodeState.Failure;
    }
}
