using UnityEngine;

/// <summary>
/// 条件ノード
/// </summary>
public class BTConditions : BTNode
{
    [SerializeField, Tooltip("ActionDataのScriptableObjectをアタッチ")] 
    protected ActionData action;              // 行動情報
    protected AIController.EnemyData data;    // 敵のデータ
    protected Transform target;               // プレイヤーの座標

    public override void NodeInit(AIController.EnemyData data, Transform playerTransform)
    {
        //--- 情報取得
        this.data = data;
        target = playerTransform;
    }
}
