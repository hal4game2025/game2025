using UnityEngine;

/// <summary>
/// シーケンス (子ノードが全て実行できたら成功 1つでも実行できなかったら失敗)
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Composite/Sequence")]
public class BTSequence : BTComposite
{
    /// <summary>
    /// 子ノードの初期化
    /// </summary>
    /// <param name="data"></param>
    /// <param name="playerTransform"></param>
    public override void NodeInit()
    {
        foreach (var node in nodes)
        {
            node.NodeInit();
        }
        base.NodeInit();
    }

    /// <summary>
    /// 実行処理
    /// </summary>
    /// <returns></returns>
    protected override NodeState NodeUpdate(ref AIController.EnemyData data, in Transform target)
    {
        // 実行中のノードがあるか
        if (runningNode)
        {
            // 実行
            state = runningNode.Tick(ref data, in target);
            return state;
        }

        foreach (var node in nodes)
        {
            // 実行
            state = node.Tick(ref data, in target);

            // 実行中のノードを記憶する
            if (state == NodeState.Running)
            {
                runningNode = node;
                return state;
            }
            // 失敗したら終了
            if (state == NodeState.Failure) return state;
        }

        // 全子ノードが実行されたら成功
        return NodeState.Success;
    }
}
