using UnityEngine;

/// <summary>
/// セレクター (子ノードが1つ成功したらそれ以降のノードは処理しない)
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Composite/Selector")]
public class BTSelector : BTComposite
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
    protected override NodeState NodeUpdate()
    {
        // 実行中のノードがあるか
        if (runningNode)
        {
            // 実行
            state = runningNode.Tick(data, target);
            return state;
        }

        // 子ノード達を実行
        foreach (var node in nodes)
        {
            // 実行
            state = node.Tick(data, target);

            // 実行中のノードを記憶
            if (state == NodeState.Running) runningNode = node;
            // 成功か実行中の場合それ以上子ノードを実行しない
            if (state != NodeState.Failure) return state;
        }

        // 子ノード全て実行し、成功や実行中が1つもなかったら失敗扱い
        return NodeState.Failure;
    }
}
