using UnityEngine;

/// <summary>
/// 残り体力を確認する
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Conditions/CheckHP")]
public class BTCheckHP : BTConditions
{
    [SerializeField, Tooltip("最大体力残量の割合"), Range(0f, 1f)] float max = 0f;
    [SerializeField, Tooltip("最小体力残量の割合"), Range(0f, 1f)] float min = 0f;
    float hp_percentage = 0f;

    protected override NodeState NodeUpdate()
    {
        // 残り体力の割合
        hp_percentage = data.status.HP / data.status.MaxHP;

        // 設定した体力残量内だったら成功
        if (max >= hp_percentage && hp_percentage > min)
            return NodeState.Success;

        return NodeState.Failure;
    }
}
