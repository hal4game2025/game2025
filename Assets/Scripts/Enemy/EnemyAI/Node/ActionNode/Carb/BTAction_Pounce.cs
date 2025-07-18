using UnityEngine;

/// <summary>
/// 飛びつき攻撃
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Action/Pounce")]
public class BTAction_Pounce : BTAction
{
    [SerializeField, Tooltip("移動速度")] float moveSpeed = 1.0f;
    Vector3 attackPos;    // 攻撃時に参照する座標 (追尾ではなく、攻撃時の座標へ飛びつく)

    protected override NodeState NodeUpdate(ref AIController.EnemyData data, in Transform target)
    {
        switch (data.animState)
        {
            case AIController.EnemyAnimState.None:
                break;
            case AIController.EnemyAnimState.Start:
                // 攻撃時に参照するターゲットの座標を記憶
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
                // ターゲットの座標へ移動しつつ攻撃 (攻撃コライダーはアニメーションイベントで有効化)
                data.status.transform.position = Vector3.MoveTowards(
                    data.status.transform.position,
                    new Vector3(attackPos.x, data.status.transform.position.y, attackPos.z),
                    moveSpeed * Time.deltaTime);
                
                break;
            case AIController.EnemyAnimState.End:
                state = NodeState.Success;
                break;
        }

        return state;
    }
}
