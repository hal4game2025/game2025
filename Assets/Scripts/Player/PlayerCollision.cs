using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    PlayerStatus playerStatus;
    string EnemyTag;
    string obstacleTag;

    private void Start()
    {
        playerStatus = GetComponent<PlayerStatus>();

        if (playerStatus == null)
        {
            Debug.LogError("PlayerStatusコンポーネントが見つからなかった");
        }

        EnemyTag = "Enemy";
        obstacleTag = "Obstacles";
    }


    /// <summary>
    /// もしプレイヤーと敵か障害物が衝突したら、スタン状態にする
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == EnemyTag)
            playerStatus.StunByEnemy();
        else if (collision.gameObject.tag == obstacleTag)
            playerStatus.StunByObstacle();
    }
}
