using UnityEngine;
using static BTNode;

/// <summary>
/// �u���X�U��
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Action/Bless")]
public class BTAction_Bless : BTAction
{
    [SerializeField, Tooltip("UpBless�̃A�j���i���o�[")] int blessNum = 0;
    //[SerializeField, Tooltip("�G�t�F�N�g�̃N���X")] EffectPlay effectPlay;
    //[SerializeField, Tooltip("�G�t�F�N�g��")] string effectName;
    //[SerializeField, Tooltip("�u���X�̔��ˈʒu")] Transform blessPos;

    //bool isBless = false;

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

    protected override void OnTerminate(ref AIController.EnemyData data, in Transform target)
    {
        //isBless = false;
        base.OnTerminate(ref data, in target);
    }

    protected override NodeState NodeUpdate(ref AIController.EnemyData data, in Transform target)
    {
        switch (data.animState)
        {
            case AIController.EnemyAnimState.None:
                break;
            case AIController.EnemyAnimState.Start:
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
                // �u���X�U���J�n
                // �u���X���o�Ă��Ȃ������甭��
                //if (!isBless)
                //{
                //    effectPlay.Play(effectName, blessPos.position);
                //    isBless = true;
                //}
                // �o�Ă����牽�����Ȃ�
                break;
            case AIController.EnemyAnimState.End:
                state = NodeState.Success;
                break;
        }

        return state;
    }
}
