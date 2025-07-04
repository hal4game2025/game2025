using UnityEngine;

/// <summary>
/// �c��̗͂��m�F����
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Conditions/CheckHP")]
public class BTCheckHP : BTConditions
{
    [SerializeField, Tooltip("�ő�̗͎c�ʂ̊���"), Range(0f, 1f)] float max = 0f;
    [SerializeField, Tooltip("�ŏ��̗͎c�ʂ̊���"), Range(0f, 1f)] float min = 0f;
    float hp_percentage = 0f;

    protected override NodeState NodeUpdate()
    {
        // �c��̗͂̊���
        hp_percentage = data.status.HP / data.status.MaxHP;

        // �ݒ肵���̗͎c�ʓ��������琬��
        if (max >= hp_percentage && hp_percentage > min)
            return NodeState.Success;

        return NodeState.Failure;
    }
}
