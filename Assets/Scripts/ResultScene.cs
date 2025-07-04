using UnityEngine;
using UnityEngine.InputSystem;
public class ResultScene : MonoBehaviour
{
   // [SerializeField] RectTransform checkImg; // �ړ�������UI

    // SceneManager�̃C���X�^���X�Q��
    SceneManager sceneManager;
    PlayerControls controls;
    int currentIndex = 0;

    [SerializeField]
    AudioClip bgm;

    void Start()
    {
      //  MoveCheckImgInstant();
        // �V���O���g������C���X�^���X�擾
        sceneManager = SceneManager.Instance;
        controls = new PlayerControls();

        controls.UI.Confirm.performed += Confirm;
        controls.Enable();

        SoundManager.Instance.Play(bgm, true);
    }
  
 
    void Confirm(InputAction.CallbackContext context)
    {
        if (gameObject.activeInHierarchy == false)
            return;
        sceneManager.ChangeScene("TitleScene");
    }
    void OnDestroy()
    {
        // InputAction�̃C�x���g�n���h��������
        controls.UI.Confirm.performed -= Confirm;
        controls.Disable();
    }


}
