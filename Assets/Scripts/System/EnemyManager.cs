using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyManager : SingletonMonoBehaviour<EnemyManager>
{
    [SerializeField] private EnemyStatus[] enemyStatuses;
    private bool isSceneChanged = false;
    public static bool isClear = false; // �V�[���J�ڌ�ɃN���A��Ԃ�ێ����邽�߂̃t���O
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
            isClear = true; // �N���A��Ԃ�ێ�
            isSceneChanged = true; // �V�[���J�ڃt���O�𗧂Ă�
            SceneManager.Instance.ChangeScene("ResultScene");
        }
    }
}