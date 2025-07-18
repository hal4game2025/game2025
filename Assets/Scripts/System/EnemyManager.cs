using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyManager : SingletonMonoBehaviour<EnemyManager>
{
    [SerializeField] private EnemyStatus[] enemyStatuses;
    private bool isSceneChanged = false;
    public static bool isClear = false; // シーン遷移後にクリア状態を保持するためのフラグ
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
            isClear = true; // クリア状態を保持
            isSceneChanged = true; // シーン遷移フラグを立てる
            SceneManager.Instance.ChangeScene("ResultScene");
        }
    }
}