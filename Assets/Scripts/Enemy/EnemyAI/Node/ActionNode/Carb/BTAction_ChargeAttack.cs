using System.Data.Common;
using UnityEngine;

/// <summary>
/// 突進攻撃
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Action/ChargeAttack")]
public class BTAction_ChargeAttack : BTAction
{
    [SerializeField, Tooltip("1フレームで移動する距離")] float movement = 0f;
    [SerializeField, Tooltip("モーション後に処理するノード")] BTNode node;
    Vector3 attackPos;

    protected override void OnTerminate(ref AIController.EnemyData data, in Transform target)
    {
        runningNode = null;
        base.OnTerminate(ref data, in target);
    }

    protected override NodeState NodeUpdate(ref AIController.EnemyData data, in Transform target)
    {
        // 実行中のノードがあるか
        if (runningNode)
        {
            // 実行
            state = runningNode.Tick(ref data, in target);
            return state;
        }

        switch (data.animState)
        {
            case AIController.EnemyAnimState.Start:
                //--- ターゲットの方を向く
                Transform transform = data.status.transform;
                attackPos = target.position - transform.position; // ターゲットからエネミーまでのベクトル
                attackPos.y = 0f;
                Quaternion look = Quaternion.LookRotation(attackPos);     // 回転に変換

                // 適用
                data.status.transform.rotation =
                    Quaternion.Slerp(transform.rotation, look, data.status.LookSpeed);

                // 方向ベクトルに変換
                attackPos = attackPos.normalized;
                break;

            case AIController.EnemyAnimState.Event:
                // 突進
                data.status.transform.position += attackPos * movement * Time.deltaTime;
                break;

            case AIController.EnemyAnimState.Next:
                // 突進中に壁にぶつかる か 突進モーションが終わったら
                // 追加ノード処理を行う
                runningNode = node;
                break;
        }

        return state;
    }
}
