using UnityEngine;

/// <summary>
/// �Z���N�^�[ (�q�m�[�h��1���������炻��ȍ~�̃m�[�h�͏������Ȃ�)
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Composite/Selector")]
public class BTSelector : BTComposite
{
    /// <summary>
    /// �q�m�[�h�̏�����
    /// </summary>
    /// <param name="data"></param>
    /// <param name="playerTransform"></param>
    public override void NodeInit()
    {
        foreach (var node in nodes)
        {
            node.NodeInit();
        }
        base.NodeInit();
    }

    /// <summary>
    /// ���s����
    /// </summary>
    /// <returns></returns>
    protected override NodeState NodeUpdate()
    {
        // ���s���̃m�[�h�����邩
        if (runningNode)
        {
            // ���s
            state = runningNode.Tick(data, target);
            return state;
        }

        // �q�m�[�h�B�����s
        foreach (var node in nodes)
        {
            // ���s
            state = node.Tick(data, target);

            // ���s���̃m�[�h���L��
            if (state == NodeState.Running) runningNode = node;
            // ���������s���̏ꍇ����ȏ�q�m�[�h�����s���Ȃ�
            if (state != NodeState.Failure) return state;
        }

        // �q�m�[�h�S�Ď��s���A��������s����1���Ȃ������玸�s����
        return NodeState.Failure;
    }
}
