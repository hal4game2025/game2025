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

    protected override void OnTerminate()
    {
        runningNode = null;
        base.OnTerminate();
    }

    protected override NodeState NodeUpdate()
    {
        // 実行中のノードがあるか
        if (runningNode)
        {
            // 実行
            state = runningNode.Tick(data, target);
            return state;
        }

        switch (data.animState)
        {
            case AIController.EnemyAnimState.None:
                break;
            case AIController.EnemyAnimState.Start:
                attackPos = target.position;
                //--- ターゲットの方を向く
                Transform transform = data.status.transform;
                Vector3 pos = target.position - transform.position; // ターゲットからエネミーまでのベクトル
                pos.y = 0f;
                Quaternion look = Quaternion.LookRotation(pos);     // 回転に変換
                // 適用
                data.status.transform.rotation =
                    Quaternion.Slerp(transform.rotation, look, data.status.LookSpeed);
                break;
            case AIController.EnemyAnimState.Event:
                // 突進
                data.status.transform.position = Vector3.MoveTowards(
                    data.status.transform.position,
                    new Vector3(attackPos.x, data.status.transform.position.y, attackPos.z),
                    movement * Time.deltaTime);
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
