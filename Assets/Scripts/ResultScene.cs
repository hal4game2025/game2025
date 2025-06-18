using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
public class ResultScene : MonoBehaviour
{
    [SerializeField] RectTransform checkImg; // 移動させるUI
    [SerializeField] float[] yPositions;     // 選択肢ごとのY座標（Inspectorで設定）

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
                // ゲーム終了
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                break;
        }
    }
    void OnDestroy()
    {
        // InputActionのイベントハンドラを解除
        controls.UI.Confirm.performed -= Confirm;
        //controls.UI.Up.performed -= Up;
        //controls.UI.Down.performed -= Down;
        controls.Disable();
    }


}
