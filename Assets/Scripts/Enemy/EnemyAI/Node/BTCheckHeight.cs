using UnityEngine;

/// <summary>
/// �^�[�Q�b�g�̍������m�F����
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Conditions/CheckHeight")]
public class BTCheckHeight : BTConditions
{
    [SerializeField, Tooltip("�ő卂��")] float maxHeight = 1f;
    [SerializeField, Tooltip("�ŏ�����")] float minHeight = 0f;

    protected override NodeState NodeUpdate(ref AIController.EnemyData data, in Transform target)
    {
        Vector3 pos = data.status.transform.position;

        return pos.y + maxHeight >= target.position.y && target.position.y > pos.y + minHeight ?
               NodeState.Success : NodeState.Failure;
    }
}
