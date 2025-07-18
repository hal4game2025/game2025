using UnityEngine;

public class EnemyAtkCollider : MonoBehaviour
{
    [SerializeField, Tooltip("ペアのコライダー")] BoxCollider[] boxColliders;

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
