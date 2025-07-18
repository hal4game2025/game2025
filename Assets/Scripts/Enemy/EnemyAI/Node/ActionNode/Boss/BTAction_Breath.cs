using UnityEngine;

/// <summary>
/// ブレス攻撃
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Action/Breath")]
public class BTAction_Breath : BTAction
{
    [SerializeField, Tooltip("出力するエフェクトの名前")] protected string effectName = "Breath";
    [SerializeField, Tooltip("出力する座標を示すオブジェクトの添字")] protected int num = 0;

    protected override NodeState NodeUpdate(ref AIController.EnemyData data, in Transform target)
    {
        switch (data.animState)
        {
            case AIController.EnemyAnimState.Start:
                //--- ターゲットの方を向く
                Transform transform = data.status.transform;
                Vector3 pos = target.position - transform.position;
                pos.y = 0;
                Quaternion look = Quaternion.LookRotation(pos);
                // 適用
                data.status.transform.rotation =
                    Quaternion.Slerp(transform.rotation, look, data.status.LookSpeed);
                break;

            case AIController.EnemyAnimState.Event:
                data.animState = AIController.EnemyAnimState.None;
                data.effect.PlayEffect(effectName, data.effectPos[num].transform);
                break;

            case AIController.EnemyAnimState.End:
                state = NodeState.Success;
                break;
        }

        return state;
    }
}
