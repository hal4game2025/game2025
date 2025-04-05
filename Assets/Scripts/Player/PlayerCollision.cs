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
            Debug.LogError("PlayerStatus�R���|�[�l���g��������Ȃ�����");
        }

        EnemyTag = "Enemy";
        obstacleTag = "Obstacles";
    }


    /// <summary>
    /// �����v���C���[�ƓG����Q�����Փ˂�����A�X�^����Ԃɂ���
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
