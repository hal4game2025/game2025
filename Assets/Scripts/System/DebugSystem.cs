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
        Vector3 cameraFront = cam.transform.forward;

        renderer.SetPosition(0, transform.position);
        renderer.SetPosition(1, transform.position + cameraFront * range);
    }
}
