using Unity.Cinemachine;
using UnityEngine;

public class DebugSystem : MonoBehaviour
{
    [SerializeField]
    GameObject headObject;

    [SerializeField]
    CinemachineCamera cam;

    [SerializeField]
    float range;

    LineRenderer renderer;

    [SerializeField]
    SceneManager sceneManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            sceneManager.ChangeScene("TestScene");
        }
    }
}
