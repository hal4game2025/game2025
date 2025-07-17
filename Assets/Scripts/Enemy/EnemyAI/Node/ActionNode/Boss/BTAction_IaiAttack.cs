using UnityEngine;

/// <summary>
/// 居合攻撃
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Action/IaiAttack")]
public class BTAction_IaiAttack : BTAction_Normal
{
    [SerializeField, Tooltip("上段攻撃のアニメ番号")] int animNumUp = 0;
    [SerializeField, Tooltip("中段攻撃のアニメ番号")] int animNum   = 0;
    [SerializeField, Tooltip("y軸の感知範囲")] float heigth = 0f;

    protected override void OnInitialize(ref AIController.EnemyData data, in Transform target)
    {
        float checkHeigth = data.status.gameObject.transform.position.y + heigth;

        // y軸を見てエネミーがターゲットより低かったら上段攻撃
        int animNumber = checkHeigth < target.position.y ?
            animNumUp : animNum;
        
        // アニメーション再生
        data.anim.SetInteger(data.animParamName, animNumber);
        state = NodeState.Running;      // 実行中に設定
    }

    protected override void OnTerminate(ref AIController.EnemyData data, in Transform target)
    {
        // 何もしない
    }
}
