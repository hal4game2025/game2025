using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyManager : MonoBehaviour
{
    [SerializeField] private EnemyStatus[] enemyStatuses;
    private bool isSceneChanged = false;
    private void Update()
    {
        // �V�[���J�ڂ��s���Ă���ꍇ�͏������X�L�b�v
        if (isSceneChanged) return;
        // ���ׂĂ̓G��Deadflg��true������
        bool allDead = true;
        foreach (var enemyStatus in enemyStatuses)
        {
            if (enemyStatus == null || !enemyStatus.Deadflg)
            {
                allDead = false;
                break;
            }
        }

        
        // �S�����S�Ȃ�V�[���J��
        if (allDead)
        {
            isSceneChanged = true; // �V�[���J�ڃt���O�𗧂Ă�
            SceneManager.Instance.ChangeScene("ResultScene");
        }
    }
}
