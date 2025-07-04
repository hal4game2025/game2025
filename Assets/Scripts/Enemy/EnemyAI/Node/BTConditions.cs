using UnityEngine;

/// <summary>
/// �����m�[�h
/// </summary>
public class BTConditions : BTNode
{
    [SerializeField, Tooltip("ActionData��ScriptableObject���A�^�b�`")] 
    protected ActionData action;              // �s�����
    protected AIController.EnemyData data;    // �G�̃f�[�^
    protected Transform target;               // �v���C���[�̍��W

    public override void NodeInit(AIController.EnemyData data, Transform playerTransform)
    {
        //--- ���擾
        this.data = data;
        target = playerTransform;
    }
}
