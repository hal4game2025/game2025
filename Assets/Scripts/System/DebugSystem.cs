using Unity.Cinemachine;
using UnityEngine;

public class DebugSystem : MonoBehaviour
{
    [SerializeField]
    SceneManager sceneManager;
    [SerializeField]
    string sceneName = "TestScene";


    private void Start()
    {
       sceneManager = SceneManager.Instance;
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
           sceneManager.ChangeScene(sceneName);
        }
    }
}
