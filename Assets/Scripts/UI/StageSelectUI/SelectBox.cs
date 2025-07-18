using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectBox : MonoBehaviour
{
    Vector3[] positions;
    StageIcon[] stageIcons;  // ステージアイコンの配列

    int index = 0;  // 現在の選択インデックス

    [SerializeField] Texture2D[] HpTex = new Texture2D[2];
    [SerializeField] RawImage[] HpUI = new RawImage[20];
    private RawImage[] playerHp;

    [SerializeField] int[] playerHP = new int[3]; // 各ステージのプレイヤーHPの最大値

    [SerializeField] Image[] resultImage = new Image[3];
    [SerializeField] Sprite[] senmetuSprite = new Sprite[3];
    [SerializeField] Sprite[] hidanSprite = new Sprite[3];
    [SerializeField] Sprite[] bairituSprite = new Sprite[3];


    private bool flag = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        positions = new Vector3[StageSelect.MAX_STAGE_COUNT];  // ステージ数分の位置を格納する配列を初期化
        stageIcons = new StageIcon[StageSelect.MAX_STAGE_COUNT];  // ステージアイコンの配列を初期化

        for (int i = 0; i < StageSelect.Instance.StageIcons.Length; i++)
        {
            positions[i] = StageSelect.Instance.StageIcons[i].transform.position;
            stageIcons[i] = StageSelect.Instance.StageIcons[i].GetComponent<StageIcon>();  // ステージアイコンの配列を取得
        }

        transform.SetSiblingIndex(3);  // 選択ボックスを最前面に配置
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxisRaw("Horizontal")==0)
            flag=false;

        Move();  // 選択ボックスの移動を処理

        if(Input.GetKeyDown(KeyCode.Return))
        {
            stageIcons[index].OnSelected(); // Enterキーが押されたら選択されたステージアイコンのクリックイベントを呼び出す
        }

        switch (index)
        {
            case 0:
                hpEnabled(index);
                ChangeSprite(index);
                break;
            case 1:
                hpEnabled(index);
                ChangeSprite(index);

                break;
            case 2:
                hpEnabled(index);
                ChangeSprite(index);

                break;
            case 3:
                hpEnabled(index);
                ChangeSprite(index);

                break;

        }

    }

    private void Move()
    {
        // TODO: コントローラー欲しいならやっといて
        if (Input.GetAxisRaw("Horizontal")<0&&!flag)
        {
            index--;
            flag = true;  // 左に移動したフラグを立てる
            
        }
        
        else if (Input.GetAxisRaw("Horizontal") > 0 && !flag)
        {
            index++;
            flag = true;  // 左に移動したフラグを立てる

        }

        if (index < 0)
        {
            index = StageSelect.MAX_STAGE_COUNT - 1;  // 最後のステージに戻る
        }
        else if (index >= StageSelect.MAX_STAGE_COUNT)
        {
            index = 0;  // 最初のステージに戻る
        }

        transform.position = positions[index];  // 選択ボックスの位置を更新
    }

    private void hpEnabled(int index)
    {
        for(int i=0; i < playerHP[index]; i++)
        {
            HpUI[i].enabled = true;
            HpUI[i].texture = HpTex[1]; // HPがある部分は有効化
        }
        for (int i = playerHP[index]; i < 20; i++)
        {
            HpUI[i].enabled = false;
        }
    }

    private void ChangeSprite(int index)
    {
        resultImage[0].sprite = senmetuSprite[index];
        resultImage[1].sprite = hidanSprite[index];
        resultImage[2].sprite = bairituSprite[index];
    }
}
