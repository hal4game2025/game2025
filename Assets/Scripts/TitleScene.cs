using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
public class TitleScene : MonoBehaviour
{
    [SerializeField] RectTransform checkImg; // 移動させるUI
    [SerializeField] RectTransform[] yPositions;
    // SceneManagerのインスタンス参照
    SceneManager sceneManager;
    PlayerControls controls;
    int currentIndex = 0;
    string StageSelect = "StageSelect"; // ステージ選択シーンの名前
    void Start()
    {
        MoveCheckImgInstant();
        // シングルトンからインスタンス取得
        sceneManager = SceneManager.Instance;
        controls = new PlayerControls();

        // InputActionのイベントハンドラを登録
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


    // InputActionのコールバックメソッド
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
                // ゲーム終了
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE // Unityエディタ以外の環境では、アプリケーションを終了する
                
                Application.Quit(); 
#endif
                break;
        }
    }
    void OnDestroy()
    {
        // InputActionのイベントハンドラを解除
        controls.UI.Confirm.performed -= Confirm;
        controls.UI.Up.performed -= Up;
        controls.UI.Down.performed -= Down;
        controls.Disable();
    }


}
