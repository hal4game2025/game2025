using UnityEngine;

public class EnemyAtkCollider : MonoBehaviour
{
    EnemyStatus status;

    void Start()
    {
        // �e�I�u�W�F�N�g����X�N���v�g���擾���āA�擾�ł��Ă������m�F
        status = GetComponentInParent<EnemyStatus>();
        if (!status) Debug.Log("EnemyAtkCollider �G�̃X�e�[�^�X�擾���s");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return;

        // �X�N���v�g�擾
        var script = other.gameObject.GetComponent<PlayerStatus>();
        if (!script) Debug.Log("�v���C���[�Ƃ̏Փ˃G���[");
        // ����10�_�� �������p���Ȃ͂�
        script.TakeDamage(10);
    }
}
