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
        foreach (var init in actionDatas)
        {
            init.SettingCD();
        }
        base.NodeInit();
    }

    protected override NodeState NodeUpdate()
    {
        // CD�X�V
        foreach (var data in actionDatas)
        {
            data.currentCD -= Time.deltaTime;
            if (data.currentCD < 0) data.currentCD = 0f;
        }
        
        // ��ɐ���
        return NodeState.Success;
    }
}
