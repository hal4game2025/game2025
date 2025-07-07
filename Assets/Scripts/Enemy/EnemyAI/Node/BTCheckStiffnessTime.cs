using UnityEngine;

/// <summary>
/// �d�����Ԃ��`�F�b�N
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Conditions/CheckStiffnessTime")]
public class BTCheckStiffnessTime : BTConditions
{
    protected override NodeState NodeUpdate()
    {
        if (data.status.stiffnessTime > 0f) data.status.stiffnessTime -= Time.deltaTime;

        // �d�����Ԃ�0���傫�������玸�s
        return data.status.stiffnessTime > 0f ? NodeState.Failure : NodeState.Success;
    }
}
