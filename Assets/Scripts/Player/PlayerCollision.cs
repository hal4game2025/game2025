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
            Debug.LogError("PlayerStatus�R���|�[�l���g��������Ȃ�����");
        }

        EnemyTag = "Enemy";
        obstacleTag = "Obstacles";
        ItemTag = "Item";
    }


    /// <summary>
    /// �����v���C���[�ƓG����Q�����Փ˂�����A�X�^����Ԃɂ���
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
            Debug.Log("�A�C�e���폜");
            Destroy(collision.gameObject);
        }
    }


    
}
