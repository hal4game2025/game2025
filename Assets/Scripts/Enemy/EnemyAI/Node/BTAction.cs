using UnityEngine;

public class BTAction : BTNode
{
    [SerializeField, Tooltip("ActionDataのScriptableObjectをアタッチ")]
    protected ActionData action;        // 行動情報

    protected override void OnInitialize()
    {
        // アニメーション再生
        data.anim.SetInteger(data.animParamName, action.AnimNum);
        state = NodeState.Running;      // 実行中に設定
        data.status.stiffnessTime = 0;  // 硬直時間をリセット
    }

    protected override void OnTerminate()
    {
        data.status.stiffnessTime = action.StiffnessTime;               // 硬直時間設定
        data.anim.SetInteger(data.animParamName, data.animIdel);        // 待機モーション再生
    }
}
