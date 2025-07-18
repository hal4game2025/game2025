using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyManager : MonoBehaviour
{
    [SerializeField] private EnemyStatus[] enemyStatuses;
    private bool isSceneChanged = false;
    private void Update()
    {
        // シーン遷移が行われている場合は処理をスキップ
        if (isSceneChanged) return;
        // すべての敵のDeadflgがtrueか判定
        bool allDead = true;
        foreach (var enemyStatus in enemyStatuses)
        {
            if (enemyStatus == null || !enemyStatus.Deadflg)
            {
                allDead = false;
                break;
            }
        }

        
        // 全員死亡ならシーン遷移
        if (allDead)
        {
            isSceneChanged = true; // シーン遷移フラグを立てる
            SceneManager.Instance.ChangeScene("ResultScene");
        }
    }
}
