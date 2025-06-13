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
        // �}�E�X�g����悤�ɐݒ�
        UnityEngine.Cursor.visible = true;
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        stageSelectUIDocument = GetComponent<UIDocument>();

        // VisualElement �̃��[�g�擾
        VisualElement root = stageSelectUIDocument.rootVisualElement;

        // �{�^���擾�iUXML �Ŏw�肵�� name �Ɉ�v������j
        var stage1Button = root.Q<Button>("Stage1");
        var stage2Button = root.Q<Button>("Stage2");

        // �C�x���g�o�^
        stage1Button?.RegisterCallback<ClickEvent>(evt => sceneManager.ChangeScene(stage1));
        stage2Button?.RegisterCallback<ClickEvent>(evt => sceneManager.ChangeScene(stage2));

        // �V�[���}�l�[�W���[�̎擾
        sceneManager = SceneManager.Instance;
    }

    void Update()
    {
        

    }
}
