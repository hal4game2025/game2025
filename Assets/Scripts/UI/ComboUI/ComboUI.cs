using UnityEngine;
using UnityEngine.UI;

public class ComboUI : MonoBehaviour
{
    [SerializeField] PlayerStatus playerStatus;     //コンボ取得用
    [SerializeField] RawImage[] comboUI = new RawImage[4];
    [SerializeField] Texture2D[] comboTexture=new Texture2D[10];

    private int[] place = new int[4];
    private int nextNum;
    void Start()
    {
    }

    void Update()
    {
        Debug.Log("Combo: " + playerStatus.Rate);
        //千の位
        place[0] = playerStatus.Rate / 1000;
        nextNum = playerStatus.Rate % 1000;

        //百の位
        place[1] = nextNum / 100;
        nextNum = nextNum % 100;

        //十の位
        place[2] = nextNum / 10;
        //一の位
        place[3] = nextNum % 10;

        for(int i = 0; i<4; i++)
        {
            comboUI[i].texture = comboTexture[place[i]];
            comboUI[i].enabled = (playerStatus.Rate >= Mathf.Pow(10, 3 - i));
        }


    }
}

