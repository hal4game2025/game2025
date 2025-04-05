using UnityEngine;

// 子オブジェクトの衝突判定を管理するスクリプト

//2025/03/30/1:51　
//障害物にぶつかっても、たまにisCollidingがfalseになるバグが発生
//OnTriggerStayで常に判定を取ろうとしたら、なぜか当たってすらいな
//いはるか彼方にある障害物にあたる。理解ができない

public class HammerCollision : MonoBehaviour
{
    [SerializeField] float collisionCheckRadius = 0.5f;

    string obstaclesTag;
    string enemyTag;

    private void Start()
    {
        obstaclesTag = "Obstacles";
        enemyTag = "Enemy";
    }

    public bool IsColliding()
    {
        // 現在の位置を中心にして、指定した半径内にあるコライダーを取得
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, collisionCheckRadius);
       

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == obstaclesTag || hitCollider.gameObject.tag == enemyTag)
            {
                return true;
            }
        }
        return false;
    }

    // Gizmosを使用して球状の範囲を表示
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // 球の色を設定
        Gizmos.DrawWireSphere(transform.position, collisionCheckRadius); // 球を描画
    }
}

