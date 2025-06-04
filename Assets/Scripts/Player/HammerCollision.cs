using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

// 子オブジェクトの衝突判定を管理するスクリプト

//2025/03/30/1:51　
//障害物にぶつかっても、たまにisCollidingがfalseになるバグが発生
//OnTriggerStayで常に判定を取ろうとしたら、なぜか当たってすらいな
//いはるか彼方にある障害物にあたる。理解ができない

public enum CollisionType
{
    None,       // 衝突なし(空中）
    Enemy,      // 敵
    Obstacles,  // 障害物
}

public class HammerCollision : MonoBehaviour
{
    [SerializeField] float collisionCheckRadius = 0.5f;


    PlayerController playerController;

    string obstaclesTag;
    string enemyTag;
    string floorTag;

    List<EnemyCollider> enemyColliderList = new List<EnemyCollider>();

    private void Start()
    {
        obstaclesTag = "Obstacles";
        enemyTag = "Enemy";
        floorTag = "floor"; // 床のタグを設定
        playerController = GetComponentInParent<PlayerController>();
    }

    /// <summary>
    /// ぶつかっているオブジェクトの種類を判定する
    /// もし何にもぶつかっていなければ、Noneを返す
    /// 複数のオブジェクトにぶつかっていた場合は優先順位にしたがって判別
    /// 敵＞ 障害物 の順で優先
    /// </summary>
    /// <returns>enum CollisionType</returns>
    public CollisionType GetCollidingType()
    {
        //
        enemyColliderList.Clear(); // 毎回リストをクリア
        // 現在の位置を中心にして、指定した半径内にあるコライダーを取得
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, collisionCheckRadius);

        bool hitObstacle = false;
        bool hitEnemy = false;
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.gameObject.CompareTag(enemyTag))
            {
                hitEnemy = true;
                var enemyStatus = hitCollider.gameObject.GetComponent<EnemyCollider>();
                if (enemyStatus != null)
                {
                    enemyColliderList.Add(enemyStatus);
                }
            }
            else if (hitCollider.gameObject.CompareTag(obstaclesTag))
            {
                hitObstacle = true;
            }
        }

        if(hitEnemy)          return CollisionType.Enemy; // 敵に衝突
        else if (hitObstacle) return CollisionType.Obstacles; // 障害物に衝突

        return CollisionType.None; // 衝突なし
    }

    public  List<EnemyCollider> GetEnemyColliderList()
    {
        return enemyColliderList;
    }

    //Gizmosを使用して球状の範囲を表示
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // 球の色を設定
        Gizmos.DrawWireSphere(transform.position, collisionCheckRadius); // 球を描画
    }
}

