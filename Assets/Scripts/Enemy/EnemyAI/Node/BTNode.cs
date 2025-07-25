using UnityEngine;

public abstract class BTNode : ScriptableObject
{
    /// <summary>
    /// ノードの状態
    /// </summary>
    public enum NodeState
    {
        None = 0,   // 無
        Success,    // 成功
        Failure,    // 失敗
        Running     // 処理中
    }

    protected NodeState state;      // 状態
    protected BTNode runningNode;   // 処理中のノードを記憶

    /// <summary>
    /// 実行時に初期化を行う
    /// </summary>
    protected virtual void OnInitialize(ref AIController.EnemyData data, in Transform target) { }

    /// <summary>
    /// 終了処理
    /// </summary>
    protected virtual void OnTerminate(ref AIController.EnemyData data, in Transform target)
    {
        if (runningNode) runningNode = null; // 実行中のノード解放
    }

    /// <summary>
    /// ノード更新
    /// </summary>
    /// <returns></returns>
    protected virtual NodeState NodeUpdate(ref AIController.EnemyData data, in Transform target) { return NodeState.None; }

    /// <summary>
    /// ノードの初期化
    /// </summary>
    public virtual void NodeInit() 
    {
        state = NodeState.None;
        if (runningNode) runningNode = null;
    }

    /// <summary>
    /// 実行処理
    /// </summary>
    /// <returns></returns>
    public NodeState Tick(ref AIController.EnemyData data, in Transform target)
    {
        // 実行中じゃない or animStateがNextなら初期化処理を行う
        if (state != NodeState.Running || data.animState == AIController.EnemyAnimState.Next) 
            OnInitialize(ref data, in target);

        // ノード更新
        state = NodeUpdate(ref data, in target);

        // 実行中じゃなければ終了処理
        if (state != NodeState.Running) OnTerminate(ref data, target);

        return state;
    }
}
