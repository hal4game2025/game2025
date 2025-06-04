using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public class StageSelectUI : MonoBehaviour
{
    UIDocument stageSelectUIDocument;
    UnityEngine.UIElements.Button button;
    SceneManager sceneManager;

    [SerializeField] string stage1;
    [SerializeField] string stage2;
    void Start()
    {
        // マウス使えるように設定
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        stageSelectUIDocument = GetComponent<UIDocument>();

        // VisualElement のルート取得
        VisualElement root = stageSelectUIDocument.rootVisualElement;

        // ボタン取得（UXML で指定した name に一致させる）
        var stage1Button = root.Q<Button>("Stage1");
        var stage2Button = root.Q<Button>("Stage2");

        // イベント登録
        stage1Button?.RegisterCallback<ClickEvent>(evt => sceneManager.ChangeScene(stage1));
        stage2Button?.RegisterCallback<ClickEvent>(evt => sceneManager.ChangeScene(stage2));

        // シーンマネージャーの取得
        sceneManager = SceneManager.Instance;
    }

    void Update()
    {
        

    }
}
