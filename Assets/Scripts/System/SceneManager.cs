using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManager : MonoBehaviour
{
    /// <summary>
    /// �V�[���J��
    /// </summary>
    /// <param name="sceneName"></param>
    public void ChangeScene(in string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
