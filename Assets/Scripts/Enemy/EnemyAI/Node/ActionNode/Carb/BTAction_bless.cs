using UnityEngine;
using static BTNode;

/// <summary>
/// �u���X�U��
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Action/Bless")]
public class BTAction_Bless : BTAction_Breath
{
    [SerializeField, Tooltip("UpBless�̃A�j���i���o�[")] int blessNum = 0;

    protected override void OnInitialize(ref AIController.EnemyData data, in Transform target)
    {
        // ��������ɋ�����UpBless
        if (data.status.transform.position.y < target.position.y)
        {
            // �A�j���[�V�����Đ�
            data.anim.SetInteger(data.animParamName, blessNum);
            state = NodeState.Running;      // ���s���ɐݒ�
            data.status.stiffnessTime = 0;  // �d�����Ԃ����Z�b�g
        }
        else
        {
            base.OnInitialize(ref data, in target);
        }
    }
}
