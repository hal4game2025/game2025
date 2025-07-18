using UnityEngine;
using UnityEngine.UI;
public class PlayerHpUI : SingletonMonoBehaviour<PlayerHpUI>
{
    [SerializeField] Texture2D[] HpTex = new Texture2D[2];
    [SerializeField] RawImage[] HpUI = new RawImage[20];
    private RawImage[] playerHp;
    [SerializeField] PlayerStatus playerStatus;
    public static int maxHp;
    public static int playerHpCount;// プレイヤーのHPの数
    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        if (playerStatus != null)
        {
            maxHp = playerStatus.HP;
            playerHp = new RawImage[maxHp];
            for (int i = maxHp; i < HpUI.Length; i++)
            {
                if (HpUI[i] != null)
                {
                    HpUI[i].enabled = false;
                }
            }
        }
    }

    void Update()
    {
        if (playerStatus != null)
        {
            // HPが0未満の場合は0にする
            int hp = Mathf.Max(playerStatus.HP, 0);

            //HPの値をplayerHpCountに同期
            int startIndex = Mathf.Max(playerStatus.HP, 0);

            for (int i = startIndex; i < 20; i++)
            {
                if (HpUI[i] != null)
                {
                    HpUI[i].texture = HpTex[0];
                }
            }
        }
    }

    /// <summary>
    /// HPのUIを取得する
    /// </summary>
    /// <returns></returns>
    public Texture2D[] GetHPTex()
    {
        return HpTex;
    }
    public RawImage[] GetHpUI()
    {
        return HpUI;
    }

    /// <summary>
    /// HPを取得する
    /// </summary>
    /// <returns></returns>
    public int GetHp()
    {
        return maxHp;
    }
    /// <summary>
    /// プレイヤーのHPの数を取得する
    /// </summary>
    /// <returns></returns>
    public int GetPlayerHpCount()
    {
        return playerHpCount;
    }
}