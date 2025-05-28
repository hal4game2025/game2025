using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

// �q�I�u�W�F�N�g�̏Փ˔�����Ǘ�����X�N���v�g

//2025/03/30/1:51�@
//��Q���ɂԂ����Ă��A���܂�isColliding��false�ɂȂ�o�O������
//OnTriggerStay�ŏ�ɔ������낤�Ƃ�����A�Ȃ����������Ă��炢��
//���͂邩�ޕ��ɂ����Q���ɂ�����B�������ł��Ȃ�

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
        enemyStatusList.Clear(); // ���񃊃X�g���N���A

        // ���݂̈ʒu�𒆐S�ɂ��āA�w�肵�����a���ɂ���R���C�_�[���擾
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, collisionCheckRadius);


        bool isEnemyOrObstaclesTag = false;
        playerController.EnemyStatus = null; // ���Ƃ��Փ˂��Ă��Ȃ����null
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag(obstaclesTag) || hitCollider.gameObject.CompareTag(enemyTag) || hitCollider.gameObject.CompareTag("floor"))
            {
                // �G�l�~�[�ƏՓ˂����ꍇ��playerController.EnemyStatus�ɃZ�b�g
                if (hitCollider.gameObject.CompareTag(enemyTag))
                {
                    Debug.Log("�G�ƏՓ˂�������������������������������������������������������������������");
                    playerController.EnemyStatus = hitCollider.gameObject.GetComponentInParent<EnemyStatus>();
                    if(playerController.EnemyStatus != null)
                    {
                       enemyStatusList.Add(playerController.EnemyStatus);
                    }
                }
                else
                {
                    playerController.EnemyStatus = null; // �G�l�~�[�ȊO�Ȃ�null�Ƀ��Z�b�g
                }

                isEnemyOrObstaclesTag = true;
            }

        }


        return isEnemyOrObstaclesTag; // �G�l�~�[�܂��͏�Q���ɏՓ˂��Ă��邩�ǂ�����Ԃ�
    }

    public  List<EnemyStatus> GetEnemyStatusList()
    {
        return enemyStatusList;
    }

    //Gizmos���g�p���ċ���͈̔͂�\��
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // ���̐F��ݒ�
        Gizmos.DrawWireSphere(transform.position, collisionCheckRadius); // ����`��
    }
}

