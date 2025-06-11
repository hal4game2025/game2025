using System.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    PlayerStatus playerStatus;
    string EnemyTag;
    string obstacleTag;
    [SerializeField]
    [Tooltip("レートの更新間隔")]
    float rateInterval = 1; // レートの更新間隔
    bool isRunning = false;

    private void Start()
    {
        playerStatus = GetComponent<PlayerStatus>();

        GetComponent<Rigidbody>().sleepThreshold = 0.0f; // Rigidbodyのスリープ閾値を0に設定

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
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == EnemyTag)
    //        playerStatus.StunByEnemy();
    //    else if (collision.gameObject.tag == obstacleTag)
    //        playerStatus.StunByObstacle();
    //}

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == EnemyTag)
            playerStatus.StunByEnemy();
        else if (collision.gameObject.tag == obstacleTag || collision.gameObject.tag == "floor")
            playerStatus.StunByObstacle();

        if (collision.gameObject.tag == "floor")
        {
            playerStatus.isFloor = true;    // 床に接地
            Debug.Log("床に衝突中");

            if(!isRunning)
            {
                StartCoroutine(UpdateRate());
            }   
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // 床から離れたかチェック
        if (collision.gameObject.tag == "floor") playerStatus.isFloor = false;
    }

    IEnumerator UpdateRate()
    {
        isRunning = true;
        playerStatus.Rate -= 1;
        yield return new WaitForSeconds(rateInterval);        
        isRunning = false;
    }
}
