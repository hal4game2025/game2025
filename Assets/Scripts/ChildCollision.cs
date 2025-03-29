using UnityEngine;

// �q�I�u�W�F�N�g�̏Փ˔�����Ǘ�����X�N���v�g
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
        // �e�I�u�W�F�N�g�͖���
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
        // �e�I�u�W�F�N�g�͖���
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

