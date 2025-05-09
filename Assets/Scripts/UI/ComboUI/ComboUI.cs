using UnityEngine;
using UnityEngine.UIElements;

public class ComboUI : MonoBehaviour
{
    [SerializeField] PlayerStatus playerStatus;     //�R���{�擾�p
    UIDocument comboUIDocument;    

    UnityEngine.UIElements.Label combolabel;        //�e�L�X�g�\���p
    bool isReady = false;

    void Start()
    { 
        comboUIDocument = GetComponent<UIDocument>();
        combolabel = comboUIDocument.rootVisualElement.Q<Label>("ComboLabel");
    }

    void Update()
    {
        combolabel.text = "x" + playerStatus.Rate.ToString(); //�\��
    }
}

