using System.Data.Common;
using UnityEngine;

/// <summary>
/// �ːi�U��
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Action/ChargeAttack")]
public class BTAction_ChargeAttack : BTAction
{
    [SerializeField, Tooltip("1�t���[���ňړ����鋗��")] float movement = 0f;
    [SerializeField, Tooltip("���[�V������ɏ�������m�[�h")] BTNode node;
    Vector3 attackPos;

    protected override void OnTerminate(ref AIController.EnemyData data, in Transform target)
    {
        runningNode = null;
        base.OnTerminate(ref data, in target);
    }

    protected override NodeState NodeUpdate(ref AIController.EnemyData data, in Transform target)
    {
        // ���s���̃m�[�h�����邩
        if (runningNode)
        {
            // ���s
            state = runningNode.Tick(ref data, in target);
            return state;
        }

        switch (data.animState)
        {
            case AIController.EnemyAnimState.Start:
                //--- �^�[�Q�b�g�̕�������
                Transform transform = data.status.transform;
                attackPos = target.position - transform.position; // �^�[�Q�b�g����G�l�~�[�܂ł̃x�N�g��
                attackPos.y = 0f;
                Quaternion look = Quaternion.LookRotation(attackPos);     // ��]�ɕϊ�

                // �K�p
                data.status.transform.rotation =
                    Quaternion.Slerp(transform.rotation, look, data.status.LookSpeed);

                // �����x�N�g���ɕϊ�
                attackPos = attackPos.normalized;
                break;

            case AIController.EnemyAnimState.Event:
                // �ːi
                data.status.transform.position += attackPos * movement * Time.deltaTime;
                break;

            case AIController.EnemyAnimState.Next:
                // �ːi���ɕǂɂԂ��� �� �ːi���[�V�������I�������
                // �ǉ��m�[�h�������s��
                runningNode = node;
                break;
        }

        return state;
    }
}
