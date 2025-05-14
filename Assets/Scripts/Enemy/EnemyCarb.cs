using UnityEngine;

public class EnemyCarb : EnemyStateBase
{
    enum ActionKind
    {
        Attack1,    // 振り下ろす攻撃
        Attack2,    // 薙ぎ払い
        Max
    }

    [SerializeField, CustomLabel("攻撃コライダー")] Collider[] atkCollider;

    ActionKind action;
    
    protected override void EnemyStart()
    {

    }

    protected override void EnemyUpdate()
    {
        for (int i = 0; i < status.atkDatas.Length; ++i)
        {
            // minよりも小さい or maxよりも大きかったらスルー
            if (distance < status.atkDatas[i].minRange || status.atkDatas[i].maxRange < distance) continue;

            // CD確認
            if (status.atkDatas[i].nowCD > 0f) continue;

            // 行動決定
            action = (ActionKind)i;
            // 値更新
            status.atkDatas[i].nowCD = status.atkDatas[i].maxCD;    // クールダウン適用
            delay = status.atkDatas[i].delay;                       // ディレイ適用
            // ステート変更
            SetEnemyState((int)EnemyState.Action);
        }
    }

    protected override void Action()
    {
        switch (action)
        {
            case ActionKind.Attack1:
                Attack1();
                break;
            case ActionKind.Attack2:
                Attack2();
                break;
        }
    }

    
    //--- private
    /// <summary>
    /// 振り下ろす攻撃
    /// </summary>
    private void Attack1()
    {
        animator.SetInteger("Action", (int)action);
    }

    /// <summary>
    /// 薙ぎ払い
    /// </summary>
    private void Attack2()
    {

    }


    //--- public
    public void ActiveCollider(int atk)
    {
        atkCollider[atk].enabled ^= true;
    }
}
