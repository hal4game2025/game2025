using UnityEngine;

public class EnemyChageAttackCollider : MonoBehaviour
{
    EnemyStatus status;
    AIController controller;
    BoxCollider boxCollider;
    [SerializeField, Tooltip("ペアのコライダー")] BoxCollider[] boxColliders;

    bool isAtk = false;

    void Start()
    {
        // 親オブジェクトからスクリプトを取得して、取得できていたか確認
        status = GetComponentInParent<EnemyStatus>();
        controller = GetComponentInParent<AIController>();

        boxCollider = GetComponent<BoxCollider>();
        
        if (!status) Debug.Log("EnemyChageAttackCollider 敵のステータス取得失敗");
        if (!controller) Debug.Log("EnemyChageAttackCollider AIController取得失敗");
        if (!boxCollider) Debug.Log("EnemyChageAttackCollider");
    }

    public void Update()
    {
        if (isAtk && !boxCollider.enabled) isAtk = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        // 壁ならそこで終了
        if (other.gameObject.tag == "Obstacles")
        {
            controller.SetEnemyAnimState((int)AIController.EnemyAnimState.Next);
            boxCollider.enabled = false;

            // ペアのコライダーを非アクティブ化
            foreach (var boxCollider in boxColliders)
            {
                boxCollider.enabled = false;
            }
        }

        if (isAtk) return;

        // プレイヤーならダメージ処理
        if (other.gameObject.tag == "Player")
        {
            // スクリプト取得
            var script = other.gameObject.GetComponent<PlayerStatus>();
            if (!script) Debug.Log("プレイヤーとの衝突エラー");

            script.TakeDamage(3);
            isAtk = true;

            // ペアのコライダーを非アクティブ化
            foreach (var boxCollider in boxColliders)
            {
                boxCollider.enabled = false;
            }
        }
    }
}
