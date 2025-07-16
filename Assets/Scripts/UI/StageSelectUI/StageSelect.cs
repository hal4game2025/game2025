using UnityEngine;
using Cysharp.Threading.Tasks;

public class StageSelect : SingletonMonoBehaviour<StageSelect>
{
    public const int MAX_STAGE_COUNT = 4;  // 最大ステージ数

    [SerializeField]float deley = 0.5f;  // ステージアイコンの表示時間差

    [SerializeField]
    GameObject[] stageIcons = new GameObject[MAX_STAGE_COUNT];  // ステージ選択ボタンの配列

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

        }
    }

}
