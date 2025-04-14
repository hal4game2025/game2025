using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManager : MonoBehaviour
{
    /// <summary>
    /// ƒV[ƒ“‘JˆÚ
    /// </summary>
    /// <param name="sceneName"></param>
    public void ChangeScene(in string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
