using UnityEngine;

public abstract class BTNode : ScriptableObject
{
    /// <summary>
    /// �m�[�h�̏��
    /// </summary>
    public enum NodeState
    {
        None = 0,   // ��
        Success,    // ����
        Failure,    // ���s
        Running     // ������
    }

    protected NodeState state;      // ���
    protected BTNode runningNode;   // �������̃m�[�h���L��

    /// <summary>
    /// ���s���ɏ��������s��
    /// </summary>
    protected virtual void OnInitialize(ref AIController.EnemyData data, in Transform target) { }

    /// <summary>
    /// �I������
    /// </summary>
    protected virtual void OnTerminate(ref AIController.EnemyData data, in Transform target)
    {
        if (runningNode) runningNode = null; // ���s���̃m�[�h���
    }

    /// <summary>
    /// �m�[�h�X�V
    /// </summary>
    /// <returns></returns>
    protected virtual NodeState NodeUpdate(ref AIController.EnemyData data, in Transform target) { return NodeState.None; }

    /// <summary>
    /// �m�[�h�̏�����
    /// </summary>
    public virtual void NodeInit() 
    {
        state = NodeState.None;
        if (runningNode) runningNode = null;
    }

    /// <summary>
    /// ���s����
    /// </summary>
    /// <returns></returns>
    public NodeState Tick(ref AIController.EnemyData data, in Transform target)
    {
        // ���s������Ȃ� or animState��Next�Ȃ珉�����������s��
        if (state != NodeState.Running || data.animState == AIController.EnemyAnimState.Next) 
            OnInitialize(ref data, in target);

        // �m�[�h�X�V
        state = NodeUpdate(ref data, in target);

        // ���s������Ȃ���ΏI������
        if (state != NodeState.Running) OnTerminate(ref data, target);

        return state;
    }
}
