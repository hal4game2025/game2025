using UnityEngine;
using static BTNode;

/// <summary>
/// ブレス攻撃
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Action/Bless")]
public class BTAction_Bless : BTAction
{
    protected override NodeState NodeUpdate()
    {
        switch (data.animState)
        {
            case AIController.EnemyAnimState.None:
                break;
            case AIController.EnemyAnimState.Start:
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
                // ブレス攻撃開始
                break;
            case AIController.EnemyAnimState.End:
                state = NodeState.Success;
                break;
        }

        return state;
    }
}
