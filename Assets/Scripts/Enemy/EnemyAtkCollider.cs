using UnityEngine;

public class EnemyAtkCollider : MonoBehaviour
{
    EnemyStatus status;

    void Start()
    {
        // 親オブジェクトからスクリプトを取得して、取得できていたか確認
        status = GetComponentInParent<EnemyStatus>();
        if (!status) Debug.Log("EnemyAtkCollider 敵のステータス取得失敗");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return;

        // スクリプト取得
        var script = other.gameObject.GetComponent<PlayerStatus>();
        if (!script) Debug.Log("プレイヤーとの衝突エラー");
        // 仮で10ダメ ←ワンパンなはず
        script.TakeDamage(10);
    }
}
