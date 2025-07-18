using UnityEngine;

/// <summary>
/// アタッチされた行動ノードのCDを更新
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/AllCountCD")]
public class BTAllCountCD : BTNode
{
    [SerializeField, Tooltip("行動ノードを全てアタッチ")]
    ActionData[] actionDatas;

    public override void NodeInit()
    {
        // 行動ノードにCDを設定
        foreach (var actionData in actionDatas)
        {
            actionData.SettingCD();
        }
        base.NodeInit();
    }

    protected override NodeState NodeUpdate(ref AIController.EnemyData data, in Transform target)
    {
        // CD更新
        foreach (var actionData in actionDatas)
        {
            actionData.currentCD -= Time.deltaTime;
            if (actionData.currentCD < 0) actionData.currentCD = 0f;
        }
        
        // 常に成功
        return NodeState.Success;
    }
}
