using UnityEngine;

/// <summary>
/// �A�^�b�`���ꂽ�s���m�[�h��CD���X�V
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/AllCountCD")]
public class BTAllCountCD : BTNode
{
    [SerializeField, Tooltip("�s���m�[�h��S�ăA�^�b�`")]
    ActionData[] actionDatas;

    public override void NodeInit()
    {
        // �s���m�[�h��CD��ݒ�
        foreach (var actionData in actionDatas)
        {
            actionData.SettingCD();
        }
        base.NodeInit();
    }

    protected override NodeState NodeUpdate(ref AIController.EnemyData data, in Transform target)
    {
        // CD�X�V
        foreach (var actionData in actionDatas)
        {
            actionData.currentCD -= Time.deltaTime;
            if (actionData.currentCD < 0) actionData.currentCD = 0f;
        }
        
        // ��ɐ���
        return NodeState.Success;
    }
}
