using UnityEngine;
using UnityEngine.UI;
public class PlayerHpUI : SingletonMonoBehaviour<PlayerHpUI>
{
    [SerializeField] Texture2D[] HpTex = new Texture2D[2];
    [SerializeField] RawImage[] HpUI = new RawImage[20];
    private RawImage[] playerHp;
    [SerializeField] PlayerStatus playerStatus;
    public static int maxHp;
    public static int playerHpCount;// �v���C���[��HP�̐�
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
            // HP��0�����̏ꍇ��0�ɂ���
            int hp = Mathf.Max(playerStatus.HP, 0);

            //HP�̒l��playerHpCount�ɓ���
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
    /// HP��UI���擾����
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
    /// HP���擾����
    /// </summary>
    /// <returns></returns>
    public int GetHp()
    {
        return maxHp;
    }
    /// <summary>
    /// �v���C���[��HP�̐����擾����
    /// </summary>
    /// <returns></returns>
    public int GetPlayerHpCount()
    {
        return playerHpCount;
    }
}