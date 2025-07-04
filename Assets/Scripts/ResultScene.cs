using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ResultScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resultTimeText; // ���U���g�^�C���\���p
    [SerializeField] TextMeshProUGUI resultHpText; // ���U���g�X�R�A�\���p
     
    private string playerHP = "20"; // �v���C���[��HP�̍ő�l

    Texture2D[] HpTex;
    RawImage[] HpUI;
    TimeSystem timeSystem;
    SceneManager sceneManager;
    PlayerControls controls;
    PlayerHpUI playerHpUI;

    [SerializeField]
    AudioClip bgm;

    void Start()
    {
        sceneManager = SceneManager.Instance;
        timeSystem = TimeSystem.Instance;
        playerHpUI = PlayerHpUI.Instance;

        controls = new PlayerControls();

        controls.UI.Confirm.performed += Confirm;
        controls.Enable();

        SoundManager.Instance.Play(bgm, true);

        //HP��\�����邽�߂̃��\�b�h�Ăяo��
        int HP = playerHpUI.GetHp(); // HP���擾���ĕ\�����邽�߂̃��\�b�h�Ăяo��
        HpTex = playerHpUI.GetHPTex(); // HP��UI�f�[�^���擾����
        HpUI = playerHpUI.GetHpUI(); // HP��UI�����擾����




        //�����Ń��U���g�^�C����HP��\��
        if (timeSystem != null && resultTimeText != null)
        {
            float time = timeSystem.GetTime();
            Debug.Log("ResultScene: Time = " + time);
            System.TimeSpan span = new System.TimeSpan(0, 0, (int)time);
            resultTimeText.text = span.ToString(@"mm\:ss");
            timeSystem.ResetTime(); //���U���g�\����Ɏ��Ԃ����Z�b�g


            for (int i = playerHpUI.GetPlayerHpCount(); i < 20; i++)
            {
                HpUI[i].texture = HpTex[0];

            }
            // HP�̕\��
            if (resultHpText != null)
            {
                resultHpText.text = playerHpUI.GetPlayerHpCount().ToString() + " / " + playerHpUI.GetHp().ToString();
            }
        }
    }

    void Confirm(InputAction.CallbackContext context)
    {
        if (gameObject.activeInHierarchy == false)
            return;


        sceneManager.ChangeScene("TitleScene");
    }

    void OnDestroy()
    {
        controls.UI.Confirm.performed -= Confirm;
        controls.Disable();
    }
}
