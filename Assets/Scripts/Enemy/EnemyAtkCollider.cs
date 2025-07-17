using UnityEngine;

public class EnemyAtkCollider : MonoBehaviour
{
    EnemyStatus status;
    [SerializeField, Tooltip("�y�A�̃R���C�_�[")] BoxCollider[] boxColliders;

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

        script.TakeDamage(3);

        // �y�A�̃R���C�_�[���A�N�e�B�u��
        foreach (var boxCollider in boxColliders)
        {
            boxCollider.enabled = false;
        }
    }
}
