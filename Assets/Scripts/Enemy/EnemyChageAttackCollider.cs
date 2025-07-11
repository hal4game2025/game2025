using UnityEngine;

public class EnemyChageAttackCollider : MonoBehaviour
{
    EnemyStatus status;
    AIController controller;
    BoxCollider boxCollider;

    bool isAtk = false;

    void Start()
    {
        // �e�I�u�W�F�N�g����X�N���v�g���擾���āA�擾�ł��Ă������m�F
        status = GetComponentInParent<EnemyStatus>();
        controller = GetComponentInParent<AIController>();

        boxCollider = GetComponent<BoxCollider>();
        
        if (!status) Debug.Log("EnemyChageAttackCollider �G�̃X�e�[�^�X�擾���s");
        if (!controller) Debug.Log("EnemyChageAttackCollider AIController�擾���s");
        if (!boxCollider) Debug.Log("EnemyChageAttackCollider");
    }

    public void Update()
    {
        if (isAtk && !boxCollider.enabled) isAtk = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        // �ǂȂ炻���ŏI��
        if (other.gameObject.tag == "Obstacles")
        {
            controller.SetEnemyAnimState((int)AIController.EnemyAnimState.Next);
            boxCollider.enabled = false;
        }

        if (isAtk) return;

        // �v���C���[�Ȃ�_���[�W����
        if (other.gameObject.tag == "Player")
        {
            // �X�N���v�g�擾
            var script = other.gameObject.GetComponent<PlayerStatus>();
            if (!script) Debug.Log("�v���C���[�Ƃ̏Փ˃G���[");

            //other.gameObject.SetActive(false); // �v���C���[���\��
            // ����10�_�� �������p���Ȃ͂�
            script.TakeDamage(10);
            isAtk = true;
        }
    }
}
