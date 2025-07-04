using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/BT")]
public class AIBehaviorTree : ScriptableObject
{
    /// <summary>
    /// BT�̈�ԏ�̃m�[�h
    /// </summary>
    [SerializeField] BTNode rootNode;

    /// <summary>
    /// BT������
    /// </summary>
    /// <param name="data"></param>
    /// <param name="playerTransform"></param>
    public void BTInit(AIController.EnemyData data, Transform playerTransform)
    {
        rootNode.NodeInit(data, playerTransform);
    }

    /// <summary>
    /// BT�X�V
    /// </summary>
    /// <returns></returns>
    public BTNode.NodeState BTUpdate()
    {
        return rootNode.Tick();
    }
}
