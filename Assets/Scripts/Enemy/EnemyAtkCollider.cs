using UnityEngine;

public class EnemyAtkCollider : MonoBehaviour
{
    EnemyStatus status;
    [SerializeField, Tooltip("ペアのコライダー")] BoxCollider[] boxColliders;

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

        script.TakeDamage(3);

        // ペアのコライダーを非アクティブ化
        foreach (var boxCollider in boxColliders)
        {
            boxCollider.enabled = false;
        }
    }
}
