using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ResultScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resultTimeText; // リザルトタイム表示用
    TimeSystem timeSystem;
    SceneManager sceneManager;
    PlayerControls controls;

    void Start()
    {
        sceneManager = SceneManager.Instance;
        timeSystem = TimeSystem.Instance;
        controls = new PlayerControls();

        controls.UI.Confirm.performed += Confirm;
        controls.Enable();


        if (timeSystem != null && resultTimeText != null)
        {
            float time = timeSystem.GetTime();
            Debug.Log("ResultScene: Time = " + time);
            System.TimeSpan span = new System.TimeSpan(0, 0, (int)time);
            resultTimeText.text = span.ToString(@"mm\:ss");
        }
    }

    void Confirm(InputAction.CallbackContext context)
    {
        if (gameObject.activeInHierarchy == false)
            return;
        sceneManager.ChangeScene("TitleScene");
    }

    void OnDestroy()
    {
        controls.UI.Confirm.performed -= Confirm;
        controls.Disable();
    }
}
