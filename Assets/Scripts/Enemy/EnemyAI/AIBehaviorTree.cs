using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/BT")]
public class AIBehaviorTree : ScriptableObject
{
    /// <summary>
    /// BTの一番上のノード
    /// </summary>
    [SerializeField] BTNode rootNode;

    /// <summary>
    /// BT初期化
    /// </summary>
    /// <param name="data"></param>
    /// <param name="playerTransform"></param>
    public void BTInit()
    {
        rootNode.NodeInit();
    }

    /// <summary>
    /// BT更新
    /// </summary>
    /// <returns></returns>
    public BTNode.NodeState BTUpdate(ref AIController.EnemyData data, in Transform target)
    {
        return rootNode.Tick(ref data, in target);
    }
}
