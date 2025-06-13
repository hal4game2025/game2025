using System.Collections;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    PlayerStatus playerStatus;
    string EnemyTag;
    string obstacleTag;
    [SerializeField]
    [Tooltip("���[�g�̍X�V�Ԋu")]
    float rateInterval = 1; // ���[�g�̍X�V�Ԋu
    bool isRunning = false;

    private void Start()
    {
        playerStatus = GetComponent<PlayerStatus>();

        GetComponent<Rigidbody>().sleepThreshold = 0.0f; // Rigidbody�̃X���[�v臒l��0�ɐݒ�

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
            playerStatus.isFloor = true;    // ���ɐڒn
            Debug.Log("���ɏՓ˒�");

            if(!isRunning)
            {
                StartCoroutine(UpdateRate());
            }   
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // �����痣�ꂽ���`�F�b�N
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
