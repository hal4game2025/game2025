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

    protected override void OnTerminate()
    {
        runningNode = null;
        base.OnTerminate();
    }

    protected override NodeState NodeUpdate()
    {
        // ���s���̃m�[�h�����邩
        if (runningNode)
        {
            // ���s
            state = runningNode.Tick(data, target);
            return state;
        }

        switch (data.animState)
        {
            case AIController.EnemyAnimState.None:
                break;
            case AIController.EnemyAnimState.Start:
                attackPos = target.position;
                //--- �^�[�Q�b�g�̕�������
                Transform transform = data.status.transform;
                Vector3 pos = target.position - transform.position; // �^�[�Q�b�g����G�l�~�[�܂ł̃x�N�g��
                pos.y = 0f;
                Quaternion look = Quaternion.LookRotation(pos);     // ��]�ɕϊ�
                // �K�p
                data.status.transform.rotation =
                    Quaternion.Slerp(transform.rotation, look, data.status.LookSpeed);
                break;
            case AIController.EnemyAnimState.Event:
                // �ːi
                data.status.transform.position = Vector3.MoveTowards(
                    data.status.transform.position,
                    new Vector3(attackPos.x, data.status.transform.position.y, attackPos.z),
                    movement * Time.deltaTime);
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
