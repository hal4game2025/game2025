using System;
using UnityEngine;

/// <summary>
/// 居合待機
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Action/Iai_Stance")]
public class BTAction_Iai : BTAction
{
    [SerializeField, Tooltip("居合待機状態から処理するノード")]
    BTNode node;

    protected override void OnTerminate(ref AIController.EnemyData data, in Transform target)
    {
        runningNode = null;
        base.OnTerminate(ref data, in target);
    }

    protected override NodeState NodeUpdate(ref AIController.EnemyData data, in Transform target)
    {
        // 実行中ノードの確認
        if (runningNode)
        {
            state = runningNode.Tick(ref data, in target);
            return state;
        }

        //--- ターゲットの方を向く
        Transform transform = data.status.transform;
        Vector3 pos = target.position - transform.position;
        pos.y = 0;
        Quaternion look = Quaternion.LookRotation(pos);
        // 適用
        data.status.transform.rotation =
            Quaternion.Slerp(transform.rotation, look, data.status.LookSpeed);

        switch (data.animState)
        {
            case AIController.EnemyAnimState.Start:
                // 子ノードの処理
                state = node.Tick(ref data, in target);

                if (state == NodeState.Running) runningNode = node;
                // 子ノードの判断で失敗した時は実行中に変換
                if (state == NodeState.Failure) state = NodeState.Running;
                break;

            case AIController.EnemyAnimState.End:
                // 待機状態が終了したら攻撃しない
                state = NodeState.Success;
                break;
        }

        return state;
    }
}
