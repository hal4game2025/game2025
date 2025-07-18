using UnityEngine;

/// <summary>
/// �V�[�P���X (�q�m�[�h���S�Ď��s�ł����琬�� 1�ł����s�ł��Ȃ������玸�s)
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/Composite/Sequence")]
public class BTSequence : BTComposite
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
    protected override NodeState NodeUpdate(ref AIController.EnemyData data, in Transform target)
    {
        // ���s���̃m�[�h�����邩
        if (runningNode)
        {
            // ���s
            state = runningNode.Tick(ref data, in target);
            return state;
        }

        foreach (var node in nodes)
        {
            // ���s
            state = node.Tick(ref data, in target);

            // ���s���̃m�[�h���L������
            if (state == NodeState.Running)
            {
                runningNode = node;
                return state;
            }
            // ���s������I��
            if (state == NodeState.Failure) return state;
        }

        // �S�q�m�[�h�����s���ꂽ�琬��
        return NodeState.Success;
    }
}
