using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
public class ResultScene : MonoBehaviour
{
    [SerializeField] RectTransform checkImg; // �ړ�������UI
    [SerializeField] float[] yPositions;     // �I�������Ƃ�Y���W�iInspector�Őݒ�j

    // SceneManager�̃C���X�^���X�Q��
    SceneManager sceneManager;
    PlayerControls controls;
    int currentIndex = 0;

    void Start()
    {
      //  MoveCheckImgInstant();
        // �V���O���g������C���X�^���X�擾
        sceneManager = SceneManager.Instance;
        controls = new PlayerControls();

        controls.UI.Confirm.performed += Confirm;
        //controls.UI.Up.performed += Up;
        //controls.UI.Down.performed += Down;
        controls.Enable();
    }

    //void MoveCheckImgInstant()
    //{
    //    if (checkImg != null && yPositions != null && currentIndex < yPositions.Length)
    //    {
    //        var pos = checkImg.anchoredPosition;
    //        checkImg.anchoredPosition = new Vector2(pos.x, yPositions[currentIndex]);
    //    }
    //}
    //void Up(InputAction.CallbackContext context)
    //{
    //    if (gameObject.activeInHierarchy == false)
    //        return;
    //    currentIndex = Mathf.Max(0, currentIndex - 1);
    //    MoveCheckImgInstant();

    //}
    //void Down(InputAction.CallbackContext context)
    //{
    //    if (gameObject.activeInHierarchy == false)
    //        return;

    //    currentIndex = Mathf.Min(yPositions.Length - 1, currentIndex + 1);
    //    MoveCheckImgInstant();
    //}
    void Confirm(InputAction.CallbackContext context)
    {
        if (gameObject.activeInHierarchy == false)
            return;
        switch (currentIndex)
        {
            case 0:
                if (sceneManager != null)
                {
                    sceneManager.ChangeScene("TitleScene");
                }
                break;
            case 1:
                // �Q�[���I��
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                break;
        }
    }
    void OnDestroy()
    {
        // InputAction�̃C�x���g�n���h��������
        controls.UI.Confirm.performed -= Confirm;
        //controls.UI.Up.performed -= Up;
        //controls.UI.Down.performed -= Down;
        controls.Disable();
    }


}
