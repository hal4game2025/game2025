using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ResultScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resultTimeText; // リザルトタイム表示用
    [SerializeField] TextMeshProUGUI resultHpText; // リザルトスコア表示用
     
    private string playerHP = "20"; // プレイヤーのHPの最大値

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

        //HPを表示するためのメソッド呼び出し
        int HP = playerHpUI.GetHp(); // HPを取得して表示するためのメソッド呼び出し
        HpTex = playerHpUI.GetHPTex(); // HPのUIデータを取得する
        HpUI = playerHpUI.GetHpUI(); // HPのUI数を取得する




        //ここでリザルトタイムとHPを表示
        if (timeSystem != null && resultTimeText != null)
        {
            float time = timeSystem.GetTime();
            Debug.Log("ResultScene: Time = " + time);
            System.TimeSpan span = new System.TimeSpan(0, 0, (int)time);
            resultTimeText.text = span.ToString(@"mm\:ss");
            timeSystem.ResetTime(); //リザルト表示後に時間をリセット


            for (int i = playerHpUI.GetPlayerHpCount(); i < 20; i++)
            {
                HpUI[i].texture = HpTex[0];

            }
            // HPの表示
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
