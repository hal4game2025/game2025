using UnityEngine;
using static BTNode;

/// <summary>
/// ブレス攻撃
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Action/Bless")]
public class BTAction_Bless : BTAction_Breath
{
    [SerializeField, Tooltip("UpBlessのアニメナンバー")] int blessNum = 0;

    protected override void OnInitialize(ref AIController.EnemyData data, in Transform target)
    {
        // 自分より上に居たらUpBless
        if (data.status.transform.position.y < target.position.y)
        {
            // アニメーション再生
            data.anim.SetInteger(data.animParamName, blessNum);
            state = NodeState.Running;      // 実行中に設定
            data.status.stiffnessTime = 0;  // 硬直時間をリセット
        }
        else
        {
            base.OnInitialize(ref data, in target);
        }
    }
}
