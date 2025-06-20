using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class StageSelectUI : MonoBehaviour
{
    [SerializeField] Image[] stageUI = new Image[2];
    [SerializeField] Image cursorUI;
    [SerializeField] int maxStage;
    [SerializeField] string[] stageName = new string[2];

    SceneManager sceneManager;

    int currentIndex = 0;

    [SerializeField]
    AudioClip bgm;

    void Start()
    {
        sceneManager = SceneManager.Instance;
        UpdateCursorPosition(); // 初期カーソル位置設定

        SoundManager.Instance.Play(bgm, true);
    }

    void Update()
    {
        var keyboard = Keyboard.current;
        var gamepad = Gamepad.current;

        // ↓ 入力（1フレームだけ）
        if ((keyboard != null && keyboard.downArrowKey.wasPressedThisFrame) ||
            (gamepad != null && gamepad.dpad.down.wasPressedThisFrame))
        {
            currentIndex = (currentIndex + 1) % maxStage;
            UpdateCursorPosition();
        }

        // ↑ 入力
        if ((keyboard != null && keyboard.upArrowKey.wasPressedThisFrame) ||
            (gamepad != null && gamepad.dpad.up.wasPressedThisFrame))
        {
            currentIndex = (currentIndex - 1 + maxStage) % maxStage;
            UpdateCursorPosition();
        }

        // 決定
        if ((keyboard != null && keyboard.enterKey.wasPressedThisFrame) ||
            (gamepad != null && gamepad.buttonSouth.wasPressedThisFrame))
        {
            sceneManager.ChangeScene(stageName[currentIndex]);
        }
    }

    void UpdateCursorPosition()
    {
        cursorUI.transform.position =new Vector3(cursorUI.transform.position.x, stageUI[currentIndex].transform.position.y);
    }
}
