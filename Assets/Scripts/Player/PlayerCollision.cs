using Unity.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    PlayerStatus playerStatus;
    string EnemyTag;
    string obstacleTag;
    string ItemTag;

    private void Start()
    {
        playerStatus = GetComponent<PlayerStatus>();

        if (playerStatus == null)
        {
            Debug.LogError("PlayerStatusコンポーネントが見つからなかった");
        }

        EnemyTag = "Enemy";
        obstacleTag = "Obstacles";
        ItemTag = "Item";
    }


    /// <summary>
    /// もしプレイヤーと敵か障害物が衝突したら、スタン状態にする
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == EnemyTag)
        {
            playerStatus.StunByEnemy();
        }    
        else if (collision.gameObject.tag == obstacleTag)
        {
            playerStatus.StunByObstacle();
        }
        else if (collision.gameObject.tag == ItemTag)
        {
            Debug.Log("アイテム削除");
            Destroy(collision.gameObject);
        }
    }


    
}
