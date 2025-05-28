using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

// 子オブジェクトの衝突判定を管理するスクリプト

//2025/03/30/1:51　
//障害物にぶつかっても、たまにisCollidingがfalseになるバグが発生
//OnTriggerStayで常に判定を取ろうとしたら、なぜか当たってすらいな
//いはるか彼方にある障害物にあたる。理解ができない

public class HammerCollision : MonoBehaviour
{
    [SerializeField] float collisionCheckRadius = 0.5f;


    PlayerController playerController;

    string obstaclesTag;
    string enemyTag;


    List<EnemyStatus> enemyStatusList = new List<EnemyStatus>();

    private void Start()
    {
        obstaclesTag = "Obstacles";
        enemyTag = "Enemy";
        playerController = GetComponentInParent<PlayerController>();
    }

    public bool IsColliding()
    {
        //
        enemyStatusList.Clear(); // 毎回リストをクリア

        // 現在の位置を中心にして、指定した半径内にあるコライダーを取得
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, collisionCheckRadius);


        bool isEnemyOrObstaclesTag = false;
        playerController.EnemyStatus = null; // 何とも衝突していなければnull
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag(obstaclesTag) || hitCollider.gameObject.CompareTag(enemyTag) || hitCollider.gameObject.CompareTag("floor"))
            {
                // エネミーと衝突した場合はplayerController.EnemyStatusにセット
                if (hitCollider.gameObject.CompareTag(enemyTag))
                {
                    Debug.Log("敵と衝突ああああああああああああああああああああああああああああああああああ");
                    playerController.EnemyStatus = hitCollider.gameObject.GetComponentInParent<EnemyStatus>();
                    if(playerController.EnemyStatus != null)
                    {
                       enemyStatusList.Add(playerController.EnemyStatus);
                    }
                }
                else
                {
                    playerController.EnemyStatus = null; // エネミー以外ならnullにリセット
                }

                isEnemyOrObstaclesTag = true;
            }

        }


        return isEnemyOrObstaclesTag; // エネミーまたは障害物に衝突しているかどうかを返す
    }

    public  List<EnemyStatus> GetEnemyStatusList()
    {
        return enemyStatusList;
    }

    //Gizmosを使用して球状の範囲を表示
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // 球の色を設定
        Gizmos.DrawWireSphere(transform.position, collisionCheckRadius); // 球を描画
    }
}

