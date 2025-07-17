using System;
using UnityEngine;

/// <summary>
/// �����ҋ@
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Action/Iai_Stance")]
public class BTAction_Iai : BTAction
{
    [SerializeField, Tooltip("�����ҋ@��Ԃ��珈������m�[�h")]
    BTNode node;

    protected override void OnTerminate(ref AIController.EnemyData data, in Transform target)
    {
        runningNode = null;
        base.OnTerminate(ref data, in target);
    }

    protected override NodeState NodeUpdate(ref AIController.EnemyData data, in Transform target)
    {
        // ���s���m�[�h�̊m�F
        if (runningNode)
        {
            state = runningNode.Tick(ref data, in target);
            return state;
        }

        //--- �^�[�Q�b�g�̕�������
        Transform transform = data.status.transform;
        Vector3 pos = target.position - transform.position;
        pos.y = 0;
        Quaternion look = Quaternion.LookRotation(pos);
        // �K�p
        data.status.transform.rotation =
            Quaternion.Slerp(transform.rotation, look, data.status.LookSpeed);

        switch (data.animState)
        {
            case AIController.EnemyAnimState.Start:
                // �q�m�[�h�̏���
                state = node.Tick(ref data, in target);

                if (state == NodeState.Running) runningNode = node;
                // �q�m�[�h�̔��f�Ŏ��s�������͎��s���ɕϊ�
                if (state == NodeState.Failure) state = NodeState.Running;
                break;

            case AIController.EnemyAnimState.End:
                // �ҋ@��Ԃ��I��������U�����Ȃ�
                state = NodeState.Success;
                break;
        }

        return state;
    }
}
