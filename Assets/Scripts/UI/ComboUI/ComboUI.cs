using UnityEngine;
using UnityEngine.UIElements;

public class ComboUI : MonoBehaviour
{
    [SerializeField] PlayerStatus playerStatus;     //�R���{�擾�p
    [SerializeField] UIDocument comboUIDocument;    

    UnityEngine.UIElements.Label combolabel;        //�e�L�X�g�\���p
    bool isReady = false;

    void Start()
    { 

        if (comboUIDocument != null)
        {
            comboUIDocument.rootVisualElement.schedule.Execute(() =>        //�Ǎ������܂������Ȃ���������GPT�̗͂��؂肽
            {
                combolabel = comboUIDocument.rootVisualElement.Q<Label>("ComboLabel");
                if (combolabel == null)
                    Debug.LogError("ComboLabel ��������܂���I");
                else
                {
                    Debug.Log("ComboLabel �擾�����I");
                    isReady = true;
                }
            });
        }
    }

    void Update()
    {
        if (isReady && playerStatus != null && combolabel != null)
        {
            combolabel.text = "�R���{��: " + playerStatus.Combo.ToString(); //�\��
        }
    }
}

