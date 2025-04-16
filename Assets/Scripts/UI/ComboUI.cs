using UnityEngine;
using UnityEngine.UIElements;

public class ComboUI : MonoBehaviour
{
    [SerializeField] PlayerStatus playerStatus;     //コンボ取得用
    [SerializeField] UIDocument comboUIDocument;    

    UnityEngine.UIElements.Label combolabel;        //テキスト表示用
    bool isReady = false;

    void Start()
    { 

        if (comboUIDocument != null)
        {
            comboUIDocument.rootVisualElement.schedule.Execute(() =>        //読込がうまくいかなかったからGPTの力を借りた
            {
                combolabel = comboUIDocument.rootVisualElement.Q<Label>("ComboLabel");
                if (combolabel == null)
                    Debug.LogError("ComboLabel が見つかりません！");
                else
                {
                    Debug.Log("ComboLabel 取得成功！");
                    isReady = true;
                }
            });
        }
    }

    void Update()
    {
        if (isReady && playerStatus != null && combolabel != null)
        {
            combolabel.text = "コンボ数: " + playerStatus.Combo.ToString(); //表示
        }
    }
}

