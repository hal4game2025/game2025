using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
public class ResultScene : MonoBehaviour
{
   // [SerializeField] RectTransform checkImg; // 移動させるUI

    // SceneManagerのインスタンス参照
    SceneManager sceneManager;
    PlayerControls controls;
    int currentIndex = 0;

    void Start()
    {
      //  MoveCheckImgInstant();
        // シングルトンからインスタンス取得
        sceneManager = SceneManager.Instance;
        controls = new PlayerControls();

        controls.UI.Confirm.performed += Confirm;
        controls.Enable();
    }
  
 
    void Confirm(InputAction.CallbackContext context)
    {
        if (gameObject.activeInHierarchy == false)
            return;
        sceneManager.ChangeScene("TitleScene");
    }
    void OnDestroy()
    {
        // InputActionのイベントハンドラを解除
        controls.UI.Confirm.performed -= Confirm;
        controls.Disable();
    }


}
