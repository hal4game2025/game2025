using UnityEngine;

// 子オブジェクトの衝突判定を管理するスクリプト
public class ChildCollision : MonoBehaviour
{
     bool isColliding = false;
    string obstaclesTag;
    string enemyTag;

    private void Start()
    {
        isColliding = false;
        obstaclesTag = "Obstacles";
        enemyTag = "Enemy";
    }


    void OnTriggerEnter(Collider other)
    {
        // 親オブジェクトは無視
        if (other.gameObject.tag == "Player")
            return;

        if (other.gameObject.tag == obstaclesTag ||
            other.gameObject.tag == enemyTag)
        {
            isColliding = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // 親オブジェクトは無視
        if (other.gameObject.tag == "Player")
            return;

        if (other.gameObject.tag == obstaclesTag ||
            other.gameObject.tag == enemyTag)
        {
            isColliding = false;
        }
    }

    public bool IsColliding()
    {
        return isColliding;
    }
}

