using UnityEngine;

/// <summary>
/// 条件ノード
/// </summary>
public class BTConditions : BTNode
{
    [SerializeField, Tooltip("ActionDataのScriptableObjectをアタッチ")] 
    protected ActionData action;              // 行動情報
}
