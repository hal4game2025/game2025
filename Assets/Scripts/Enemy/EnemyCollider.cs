using UnityEngine;
using static EnemyStateBase;

public class EnemyCollider : MonoBehaviour
{
    EnemyStatus status;

    void Start()
    {
        status = GetComponentInParent<EnemyStatus>();
        if (!status) Debug.Log("EnemyCollider �G�̃X�e�[�^�X�擾���s");

    }

    /// <summary>
    /// �_���[�W����
    /// </summary>
    /// <param name="damage"></param>
    public void AddDamage(float damage)
    {
        status.Damage(damage);
    }
}
