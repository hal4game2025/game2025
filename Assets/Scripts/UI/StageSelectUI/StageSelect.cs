using UnityEngine;
using Cysharp.Threading.Tasks;

public class StageSelect : SingletonMonoBehaviour<StageSelect>
{
    public const int MAX_STAGE_COUNT = 4;  // 最大ステージ数

    [SerializeField]float deley = 0.5f;  // ステージアイコンの表示時間差

    [SerializeField]
    GameObject[] stageIcons = new GameObject[MAX_STAGE_COUNT];  // ステージ選択ボタンの配列

    [SerializeField]
    GameObject linePrefab;
    [SerializeField] Transform UICanvas;


#if UNITY_EDITOR
    Line[] lines = new Line[3];  // エディターでのデバッグ用にラインを格納する配列
#endif

    public GameObject[] StageIcons
    {
        get { return stageIcons; }
        private set { stageIcons = value; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(stageIcons.Length != MAX_STAGE_COUNT)
        {
            Debug.LogError("ステージ数が多い");
        }

        ShowStageIcons();  // 時間差でステージアイコンを表示するメソッドを呼び出す
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.T))
        {
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] != null)
                {
                    Destroy(lines[i].gameObject);  // エディターでのデバッグ用にラインを削除
                }
            }

            ShowStageIcons();  // Enterキーが押されたらステージアイコンを再表示
        }
#endif
    }

    // 時間差でステージアイコンを表示するメソッド
    async void ShowStageIcons()
    {
        for (int i = 0; i < stageIcons.Length; i++)
        {
            stageIcons[i].SetActive(false);  // ステージアイコンを非表示にする
        }


        for (int i = 0; i < stageIcons.Length; i++)
        {
            if(i != 0)
            {
                await UniTask.Delay((int)(deley * 1000));
            }

            stageIcons[i].SetActive(true);  // ステージアイコンを表示

            if (i < stageIcons.Length - 1)
            {
                CreateLine(i);
            }
        }
    }


    async void CreateLine(int index)
    {

        await UniTask.Delay((int)(deley * 1000));


        Line line = Instantiate(linePrefab, UICanvas).GetComponent<Line>();
#if UNITY_EDITOR
        lines[index] = line;  // エディターでのデバッグ用にラインを格納
#endif

        await UniTask.Yield(); // 1フレーム待つ
        line.Active(stageIcons[index].transform.position, stageIcons[index + 1].transform.position);
    }
}
