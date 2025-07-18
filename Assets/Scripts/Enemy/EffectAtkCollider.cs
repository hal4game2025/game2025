using UnityEngine;

public class EffectAtkCollider : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return;

        // �X�N���v�g�擾
        var script = other.gameObject.GetComponent<PlayerStatus>();
        if (!script) Debug.Log("�v���C���[�Ƃ̏Փ˃G���[");

        script.TakeDamage(3);
    }
}
