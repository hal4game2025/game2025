using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

// �q�I�u�W�F�N�g�̏Փ˔�����Ǘ�����X�N���v�g

//2025/03/30/1:51�@
//��Q���ɂԂ����Ă��A���܂�isColliding��false�ɂȂ�o�O������
//OnTriggerStay�ŏ�ɔ������낤�Ƃ�����A�Ȃ����������Ă��炢��
//���͂邩�ޕ��ɂ����Q���ɂ�����B�������ł��Ȃ�

public enum CollisionType
{
    None,       // �Փ˂Ȃ�(�󒆁j
    Enemy,      // �G
    Obstacles,  // ��Q��
}

public class HammerCollision : MonoBehaviour
{
    [SerializeField] float collisionCheckRadius = 0.5f;


    PlayerController playerController;

    string obstaclesTag;
    string enemyTag;
    string floorTag;

    List<EnemyCollider> enemyColliderList = new List<EnemyCollider>();

    private void Start()
    {
        obstaclesTag = "Obstacles";
        enemyTag = "Enemy";
        floorTag = "floor"; // ���̃^�O��ݒ�
        playerController = GetComponentInParent<PlayerController>();
    }

    /// <summary>
    /// �Ԃ����Ă���I�u�W�F�N�g�̎�ނ𔻒肷��
    /// �������ɂ��Ԃ����Ă��Ȃ���΁ANone��Ԃ�
    /// �����̃I�u�W�F�N�g�ɂԂ����Ă����ꍇ�͗D�揇�ʂɂ��������Ĕ���
    /// �G�� ��Q�� �̏��ŗD��
    /// </summary>
    /// <returns>enum CollisionType</returns>
    public CollisionType GetCollidingType()
    {
        //
        enemyColliderList.Clear(); // ���񃊃X�g���N���A
        // ���݂̈ʒu�𒆐S�ɂ��āA�w�肵�����a���ɂ���R���C�_�[���擾
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, collisionCheckRadius);

        bool hitObstacle = false;
        bool hitEnemy = false;
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.gameObject.CompareTag(enemyTag))
            {
                hitEnemy = true;
                var enemyStatus = hitCollider.gameObject.GetComponent<EnemyCollider>();
                if (enemyStatus != null)
                {
                    enemyColliderList.Add(enemyStatus);
                }
            }
            else if (hitCollider.gameObject.CompareTag(obstaclesTag))
            {
                hitObstacle = true;
            }
        }

        if(hitEnemy)          return CollisionType.Enemy; // �G�ɏՓ�
        else if (hitObstacle) return CollisionType.Obstacles; // ��Q���ɏՓ�

        return CollisionType.None; // �Փ˂Ȃ�
    }

    public  List<EnemyCollider> GetEnemyColliderList()
    {
        return enemyColliderList;
    }

    //Gizmos���g�p���ċ���͈̔͂�\��
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // ���̐F��ݒ�
        Gizmos.DrawWireSphere(transform.position, collisionCheckRadius); // ����`��
    }
}

