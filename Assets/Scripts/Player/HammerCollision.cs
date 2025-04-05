using UnityEngine;

// �q�I�u�W�F�N�g�̏Փ˔�����Ǘ�����X�N���v�g

//2025/03/30/1:51�@
//��Q���ɂԂ����Ă��A���܂�isColliding��false�ɂȂ�o�O������
//OnTriggerStay�ŏ�ɔ������낤�Ƃ�����A�Ȃ����������Ă��炢��
//���͂邩�ޕ��ɂ����Q���ɂ�����B�������ł��Ȃ�

public class HammerCollision : MonoBehaviour
{
    [SerializeField] float collisionCheckRadius = 0.5f;

    string obstaclesTag;
    string enemyTag;

    private void Start()
    {
        obstaclesTag = "Obstacles";
        enemyTag = "Enemy";
    }

    public bool IsColliding()
    {
        // ���݂̈ʒu�𒆐S�ɂ��āA�w�肵�����a���ɂ���R���C�_�[���擾
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, collisionCheckRadius);
       

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == obstaclesTag || hitCollider.gameObject.tag == enemyTag)
            {
                return true;
            }
        }
        return false;
    }

    // Gizmos���g�p���ċ���͈̔͂�\��
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // ���̐F��ݒ�
        Gizmos.DrawWireSphere(transform.position, collisionCheckRadius); // ����`��
    }
}

