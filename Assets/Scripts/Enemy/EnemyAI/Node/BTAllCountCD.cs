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
        foreach (var init in actionDatas)
        {
            init.SettingCD();
        }
        base.NodeInit();
    }

    protected override NodeState NodeUpdate()
    {
        // CD更新
        foreach (var data in actionDatas)
        {
            data.currentCD -= Time.deltaTime;
            if (data.currentCD < 0) data.currentCD = 0f;
        }
        
        // 常に成功
        return NodeState.Success;
    }
}
