using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Conditions/CheckRange")]
public class BTCheckRange : BTConditions
{
    float distance = 0f;

    protected override NodeState NodeUpdate(ref AIController.EnemyData data, in Transform target)
    {
        // ����
        distance = Vector3.Distance(target.position, data.status.gameObject.transform.position);

        // �s���͈͓��������琬����Ԃ�
        if (action.MaxRange >= distance && distance > action.MinRange)
            return NodeState.Success;

        return NodeState.Failure;
    }
}
