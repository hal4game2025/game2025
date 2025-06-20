using UnityEngine;
using static EnemyStateBase;

public class EnemyCollider : MonoBehaviour
{
    EnemyStatus status;

    void Start()
    {
        status = GetComponentInParent<EnemyStatus>();
        if (!status) Debug.Log("EnemyCollider 敵のステータス取得失敗");

    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="damage"></param>
    public void AddDamage(float damage)
    {
        status.Damage(damage);
    }
}
