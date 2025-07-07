using UnityEngine;

/// <summary>
/// 振り下ろし攻撃
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Action/Attack")]
public class BTAction_Attack : BTAction
{
    protected override NodeState NodeUpdate()
    {
        // アニメーションが終わっているかチェック (注意点：AnimClipでアニメーションの最後にEvent関数を入れる)
        if (data.animState == AIController.EnemyAnimState.End)
        {
            state = NodeState.Success;
            return state;
        }

        return state;
    }
}
