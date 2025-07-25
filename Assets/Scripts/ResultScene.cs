using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class ResultScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resultTimeText; // リザルトタイム表示用
    [SerializeField] TextMeshProUGUI resultHpText; // リザルトスコア表示用
    [SerializeField] RawImage[] resultStarIMG; // リザルト星表示用（3つ想定）

    private string playerHP = "20"; // プレイヤーのHPの最大値

    [Header("経過時間による星の入手難易度はここを変更する(星2つ目は一つめの時間 / 2)")]
    //星1のスコア時間ここを変更することで、星の獲得条件を調整できます。
    [SerializeField] float ScoreTime = 10f; 

    //private float ScoreTime2 = f; // 星1のスコア時間
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

        HpTex = playerHpUI.GetHPTex(); // HPのUIデータを取得する
        HpUI = playerHpUI.GetHpUI(); // HPのUI数を取得する

        //ここでリザルトタイムとHPを表示
        if (timeSystem != null && resultTimeText != null)
        {
            //=====リザルトタイムの表示=====
            float time = timeSystem.GetTime();
            Debug.Log("ResultScene: Time = " + time);
            System.TimeSpan span = new System.TimeSpan(0, 0, (int)time);
            resultTimeText.text = span.ToString(@"mm\:ss");
            timeSystem.ResetTime(); //リザルト表示後に時間をリセット

            //=====残HPの表示=====
            int hpCount = Mathf.Clamp(playerHpUI.GetPlayerHpCount(), 0, HpUI.Length);// プレイヤーの残りHPを取得し、UIの範囲内に制限
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

            //=====星(スコア)の表示=====
            // 星のイラストは、RawImage配列 resultStarIMG[0], [1], [2] を想定
            // すべて非表示にしてから必要な分だけ有効化
            for (int i = 0; i < resultStarIMG.Length; i++)
            {
                resultStarIMG[i].enabled = false;
            }
            
            if (playerHpUI.GetPlayerHpCount() <= 0)
            {
                // HPが0以下の場合は星を表示しない
                return;
            }


            int starCount = 0;
            // 5分(300秒)以内 → 星1
            if (time <= ScoreTime)
            {
                starCount++;
                // 2分(120秒)以内 → 星2
                if (time <= ScoreTime /2 )
                {
                    starCount++;                   
                }
            }

            // HPが満タン(減っていない)
            if (playerHpUI.GetPlayerHpCount() == playerHpUI.GetHp())
            {
                starCount++;
            }

            for (int i = 0; i < starCount && i < resultStarIMG.Length; i++)
            {
                resultStarIMG[i].enabled = true;
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