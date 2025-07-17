using UnityEngine;

/// <summary>
/// �����U��
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Action/IaiAttack")]
public class BTAction_IaiAttack : BTAction_Normal
{
    [SerializeField, Tooltip("��i�U���̃A�j���ԍ�")] int animNumUp = 0;
    [SerializeField, Tooltip("���i�U���̃A�j���ԍ�")] int animNum   = 0;
    [SerializeField, Tooltip("y���̊��m�͈�")] float heigth = 0f;

    protected override void OnInitialize(ref AIController.EnemyData data, in Transform target)
    {
        float checkHeigth = data.status.gameObject.transform.position.y + heigth;

        // y�������ăG�l�~�[���^�[�Q�b�g���Ⴉ�������i�U��
        int animNumber = checkHeigth < target.position.y ?
            animNumUp : animNum;
        
        // �A�j���[�V�����Đ�
        data.anim.SetInteger(data.animParamName, animNumber);
        state = NodeState.Running;      // ���s���ɐݒ�
    }

    protected override void OnTerminate(ref AIController.EnemyData data, in Transform target)
    {
        // �������Ȃ�
    }
}
