using UnityEngine;

/// <summary>
/// �u���X�U��
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Action/Breath")]
public class BTAction_Breath : BTAction
{
    [SerializeField, Tooltip("�o�͂���G�t�F�N�g�̖��O")] protected string effectName = "Breath";
    [SerializeField, Tooltip("�o�͂�����W�������I�u�W�F�N�g�̓Y��")] protected int num = 0;

    protected override NodeState NodeUpdate(ref AIController.EnemyData data, in Transform target)
    {
        switch (data.animState)
        {
            case AIController.EnemyAnimState.Start:
                //--- �^�[�Q�b�g�̕�������
                Transform transform = data.status.transform;
                Vector3 pos = target.position - transform.position;
                pos.y = 0;
                Quaternion look = Quaternion.LookRotation(pos);
                // �K�p
                data.status.transform.rotation =
                    Quaternion.Slerp(transform.rotation, look, data.status.LookSpeed);
                break;

            case AIController.EnemyAnimState.Event:
                data.animState = AIController.EnemyAnimState.None;
                data.effect.PlayEffect(effectName, data.effectPos[num].transform);
                break;

            case AIController.EnemyAnimState.End:
                state = NodeState.Success;
                break;
        }

        return state;
    }
}
