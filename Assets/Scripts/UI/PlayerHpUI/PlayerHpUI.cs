using UnityEngine;
using UnityEngine.UI;
public class PlayerHpUI : MonoBehaviour
{
    [SerializeField] Texture2D[] HpTex=new Texture2D[2];
    [SerializeField] RawImage[] HpUI = new RawImage[20];
    private RawImage[] playerHp;
    [SerializeField] PlayerStatus playerStatus;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int maxHp;
        maxHp = playerStatus.HP;
        playerHp = new RawImage[maxHp];
        for (int i = maxHp; i < 20; i++)
        {
            HpUI[i].enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = playerStatus.HP; i < 20; i++)
        {
            HpUI[i].texture = HpTex[0];

        }
    }
}
