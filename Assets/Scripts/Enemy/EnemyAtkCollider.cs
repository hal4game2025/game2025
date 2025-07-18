using UnityEngine;

public class EnemyAtkCollider : MonoBehaviour
{
    [SerializeField, Tooltip("�y�A�̃R���C�_�[")] BoxCollider[] boxColliders;

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
