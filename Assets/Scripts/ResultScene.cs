using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ResultScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resultTimeText; // ���U���g�^�C���\���p
    [SerializeField] TextMeshProUGUI resultHpText; // ���U���g�X�R�A�\���p
    [SerializeField] RawImage[] resultStarIMG; // ���U���g���\���p�i3�z��j

    private string playerHP = "20"; // �v���C���[��HP�̍ő�l

    [Header("�o�ߎ��Ԃɂ�鐯�̓����Փx�͂�����ύX����(��2�ڂ͈�߂̎��� / 2)")]
    //��1�̃X�R�A���Ԃ�����ύX���邱�ƂŁA���̊l�������𒲐��ł��܂��B
    [SerializeField] float ScoreTime = 10f; 

    //private float ScoreTime2 = f; // ��1�̃X�R�A����
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

        HpTex = playerHpUI.GetHPTex(); // HP��UI�f�[�^���擾����
        HpUI = playerHpUI.GetHpUI(); // HP��UI�����擾����

        //�����Ń��U���g�^�C����HP��\��
        if (timeSystem != null && resultTimeText != null)
        {
            //=====���U���g�^�C���̕\��=====
            float time = timeSystem.GetTime();
            Debug.Log("ResultScene: Time = " + time);
            System.TimeSpan span = new System.TimeSpan(0, 0, (int)time);
            resultTimeText.text = span.ToString(@"mm\:ss");
            timeSystem.ResetTime(); //���U���g�\����Ɏ��Ԃ����Z�b�g

            //=====�cHP�̕\��=====
            int hpCount = Mathf.Clamp(playerHpUI.GetPlayerHpCount(), 0, HpUI.Length);// �v���C���[�̎c��HP���擾���AUI�͈͓̔��ɐ���
            for (int i = hpCount; i < HpUI.Length; i++)
            {
                if (HpUI[i] != null)
                {
                    HpUI[i].texture = HpTex[0];
                }
            }

            if (resultHpText != null)
            {
                resultHpText.text = playerHpUI.GetPlayerHpCount().ToString() + " / " + playerHpUI.GetHp().ToString();
            }

            //=====��(�X�R�A)�̕\��=====
            // ���̃C���X�g�́ARawImage�z�� resultStarIMG[0], [1], [2] ��z��
            // ���ׂĔ�\���ɂ��Ă���K�v�ȕ������L����
            //for (int i = 0; i < resultStarIMG.Length; i++)
            //{
            //    resultStarIMG[i].enabled = false;
            //}
            
            //if (playerHpUI.GetPlayerHpCount() <= 0)
            //{
            //    // HP��0�ȉ��̏ꍇ�͐���\�����Ȃ�
            //    return;
            //}




            //int starCount = 0;
            //// 5��(300�b)�ȓ� �� ��1
            //if (time <= ScoreTime)
            //{
            //    starCount++;
            //    // 2��(120�b)�ȓ� �� ��2
            //    if (time <= ScoreTime /2 )
            //    {
            //        starCount++;                   
            //    }
            //}

            //// HP�����^��(�����Ă��Ȃ�)
            //if (playerHpUI.GetPlayerHpCount() == playerHpUI.GetHp())
            //{
            //    starCount++;
            //}

            //for (int i = 0; i < starCount && i < resultStarIMG.Length; i++)
            //{
            //    resultStarIMG[i].enabled = true;
            //}
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