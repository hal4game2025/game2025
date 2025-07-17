using UnityEngine;
using static BTNode;

/// <summary>
/// ブレス攻撃
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Action/Bless")]
public class BTAction_Bless : BTAction
{
    [SerializeField, Tooltip("UpBlessのアニメナンバー")] int blessNum = 0;
    //[SerializeField, Tooltip("エフェクトのクラス")] EffectPlay effectPlay;
    //[SerializeField, Tooltip("エフェクト名")] string effectName;
    //[SerializeField, Tooltip("ブレスの発射位置")] Transform blessPos;

    //bool isBless = false;

    protected override void OnInitialize(ref AIController.EnemyData data, in Transform target)
    {
        // 自分より上に居たらUpBless
        if (data.status.transform.position.y < target.position.y)
        {
            // アニメーション再生
            data.anim.SetInteger(data.animParamName, blessNum);
            state = NodeState.Running;      // 実行中に設定
            data.status.stiffnessTime = 0;  // 硬直時間をリセット
        }
        else
        {
            base.OnInitialize(ref data, in target);
        }
    }

    protected override void OnTerminate(ref AIController.EnemyData data, in Transform target)
    {
        //isBless = false;
        base.OnTerminate(ref data, in target);
    }

    protected override NodeState NodeUpdate(ref AIController.EnemyData data, in Transform target)
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
                // ブレスが出ていなかったら発射
                //if (!isBless)
                //{
                //    effectPlay.Play(effectName, blessPos.position);
                //    isBless = true;
                //}
                // 出ていたら何もしない
                break;
            case AIController.EnemyAnimState.End:
                state = NodeState.Success;
                break;
        }

        return state;
    }
}
