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

    protected NodeState state = NodeState.None;     // ���
    protected BTNode runningNode;                   // �������̃m�[�h���L��


    /// <summary>
    /// ���s���ɏ��������s��
    /// </summary>
    protected virtual void OnInitialize() { }

    /// <summary>
    /// �I������
    /// </summary>
    protected virtual void OnTerminate()
    {
        if (runningNode) runningNode = null; // ���s���̃m�[�h���
    }

    /// <summary>
    /// �m�[�h�X�V
    /// </summary>
    /// <returns></returns>
    protected virtual NodeState NodeUpdate() { return NodeState.None; }

    /// <summary>
    /// �m�[�h�̏�����
    /// </summary>
    public virtual void NodeInit(AIController.EnemyData data, Transform playerTransform)
    {
        
    }

    /// <summary>
    /// ���s����
    /// </summary>
    /// <returns></returns>
    public NodeState Tick()
    {
        // ���s������Ȃ���Ώ������������s��
        if (state != NodeState.Running) OnInitialize();
        // �m�[�h�X�V
        state = NodeUpdate();
        // ���s������Ȃ���ΏI������
        if (state != NodeState.Running) OnTerminate();

        return state;
    }
}
