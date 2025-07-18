using UnityEngine;

public class EffectAtkCollider : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return;

        // スクリプト取得
        var script = other.gameObject.GetComponent<PlayerStatus>();
        if (!script) Debug.Log("プレイヤーとの衝突エラー");

        script.TakeDamage(3);
    }
}
