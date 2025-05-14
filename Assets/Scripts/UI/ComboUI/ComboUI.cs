using UnityEngine;
using UnityEngine.UIElements;

public class ComboUI : MonoBehaviour
{
    [SerializeField] PlayerStatus playerStatus;     //コンボ取得用
    UIDocument comboUIDocument;    

    UnityEngine.UIElements.Label combolabel;        //テキスト表示用
    bool isReady = false;

    void Start()
    { 
        comboUIDocument = GetComponent<UIDocument>();
        combolabel = comboUIDocument.rootVisualElement.Q<Label>("ComboLabel");
    }

    void Update()
    {
        combolabel.text = "x" + playerStatus.Rate.ToString(); //表示
    }
}

