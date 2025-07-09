using UnityEngine;

public class BTAction : BTNode
{
    [SerializeField, Tooltip("ActionData��ScriptableObject���A�^�b�`")]
    protected ActionData action;        // �s�����

    protected override void OnInitialize()
    {
        // �A�j���[�V�����Đ�
        data.anim.SetInteger(data.animParamName, action.AnimNum);
        state = NodeState.Running;      // ���s���ɐݒ�
        data.status.stiffnessTime = 0;  // �d�����Ԃ����Z�b�g
    }

    protected override void OnTerminate()
    {
        data.status.stiffnessTime = action.StiffnessTime;               // �d�����Ԑݒ�
        data.anim.SetInteger(data.animParamName, data.animIdel);        // �ҋ@���[�V�����Đ�
    }
}
