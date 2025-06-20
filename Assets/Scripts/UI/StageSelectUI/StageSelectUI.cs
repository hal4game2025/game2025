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
        UpdateCursorPosition(); // �����J�[�\���ʒu�ݒ�

        SoundManager.Instance.Play(bgm, true);
    }

    void Update()
    {
        var keyboard = Keyboard.current;
        var gamepad = Gamepad.current;

        // �� ���́i1�t���[�������j
        if ((keyboard != null && keyboard.downArrowKey.wasPressedThisFrame) ||
            (gamepad != null && gamepad.dpad.down.wasPressedThisFrame))
        {
            currentIndex = (currentIndex + 1) % maxStage;
            UpdateCursorPosition();
        }

        // �� ����
        if ((keyboard != null && keyboard.upArrowKey.wasPressedThisFrame) ||
            (gamepad != null && gamepad.dpad.up.wasPressedThisFrame))
        {
            currentIndex = (currentIndex - 1 + maxStage) % maxStage;
            UpdateCursorPosition();
        }

        // ����
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
