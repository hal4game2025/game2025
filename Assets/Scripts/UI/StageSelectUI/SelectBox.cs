using UnityEngine;

public class SelectBox : MonoBehaviour
{
    Vector3[] positions;
    StageIcon[] stageIcons;  // ステージアイコンの配列

    int index = 0;  // 現在の選択インデックス

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
        Move();  // 選択ボックスの移動を処理

        if(Input.GetKeyDown(KeyCode.Return))
        {
            stageIcons[index].OnSelected(); // Enterキーが押されたら選択されたステージアイコンのクリックイベントを呼び出す
        }
    }

    private void Move()
    {
        // TODO: コントローラー欲しいならやっといて
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            index--;
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            index++;
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
}
