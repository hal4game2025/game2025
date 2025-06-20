using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
public class TitleScene : MonoBehaviour
{
    [SerializeField] RectTransform checkImg; // �ړ�������UI
    [SerializeField] RectTransform[] yPositions;
    // SceneManager�̃C���X�^���X�Q��
    SceneManager sceneManager;
    PlayerControls controls;
    int currentIndex = 0;
    string StageSelect = "StageSelect"; // �X�e�[�W�I���V�[���̖��O
    void Start()
    {
        MoveCheckImgInstant();
        // �V���O���g������C���X�^���X�擾
        sceneManager = SceneManager.Instance;
        controls = new PlayerControls();

        // InputAction�̃C�x���g�n���h����o�^
        controls.UI.Confirm.performed += Confirm;
        controls.UI.Up.performed += Up;
        controls.UI.Down.performed += Down;
        controls.Enable();
    }

    void MoveCheckImgInstant()
    {
        if (checkImg != null && yPositions != null && currentIndex < yPositions.Length)
        {
            var pos = checkImg.anchoredPosition;
            checkImg.anchoredPosition = new Vector2(pos.x, yPositions[currentIndex].anchoredPosition.y);
        }
    }


    // InputAction�̃R�[���o�b�N���\�b�h
    void Up(InputAction.CallbackContext context)
    {
        if (gameObject.activeInHierarchy == false)
            return;
        currentIndex = Mathf.Max(0, currentIndex - 1);
        MoveCheckImgInstant();

    }
    void Down(InputAction.CallbackContext context)
    {
        if (gameObject.activeInHierarchy == false)
            return;

        currentIndex = Mathf.Min(yPositions.Length - 1, currentIndex + 1);
        MoveCheckImgInstant();
    }
    void Confirm(InputAction.CallbackContext context)
    {
        if (gameObject.activeInHierarchy == false)
            return;
        switch (currentIndex)
        {
            case 0:
                if (sceneManager != null)
                {
                    sceneManager.ChangeScene(StageSelect);
                }
                break;
            case 1:
                // �Q�[���I��
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE // Unity�G�f�B�^�ȊO�̊��ł́A�A�v���P�[�V�������I������
                
                Application.Quit(); 
#endif
                break;
        }
    }
    void OnDestroy()
    {
        // InputAction�̃C�x���g�n���h��������
        controls.UI.Confirm.performed -= Confirm;
        controls.UI.Up.performed -= Up;
        controls.UI.Down.performed -= Down;
        controls.Disable();
    }


}
