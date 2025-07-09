using UnityEngine;

/// <summary>
/// ターゲットを見つめる
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/SeeTarget")]
public class BTSeeTarget : BTNode
{
    protected override NodeState NodeUpdate()
    {
        Transform transform = data.status.transform;

        //--- ターゲットの方を向く
        Vector3 pos = target.position - transform.position; // ターゲットからエネミーまでのベクトル
        pos.y = 0f;
        Quaternion look = Quaternion.LookRotation(pos);     // 回転に変換
        // 適用
        data.status.transform.rotation = 
            Quaternion.Slerp(transform.rotation, look, data.status.LookSpeed);

        return NodeState.Success;
    }
}
