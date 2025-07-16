using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneManager : SingletonMonoBehaviour<SceneManager>
{
    [SerializeField]
    Image fadeImage;

    [SerializeField]
    GameObject imagePrefab; // imageがなかったときの生成用

    FadeUtils fadeUtils;

    void Start()
    {
        if(fadeImage  == null)
        {
            
            fadeImage = Instantiate(imagePrefab).GetComponentInChildren<Image>();
        }

         

        DontDestroyOnLoad(gameObject); // シーン遷移時に破棄しないように設定
        DontDestroyOnLoad(fadeImage.canvas); // フェードキャンバスを破棄しないように設定

        fadeUtils = new FadeUtils();
        fadeUtils.fadeDuration = 1.0f; // フェードの時間を設定
    }

    void Update()
    {
        // フェードのアルファ値をUIに反映
        if (fadeUtils.IsFade)
        {
            fadeImage.color = new Color(0.0f,0.0f,0.0f,fadeUtils.Alpha);
        }
        
    }

    /// <summary>
    /// シーン遷移
    /// </summary>
    /// <param name="sceneName"></param>
    async public void ChangeScene(string sceneName)
    {

        if(!IsSceneExist(name))
        {
            Debug.LogError($"シーン {sceneName} は存在しません。");
            return;
        }

        await fadeUtils.Fade(FadeUtils.E_FADE_TYPE.FADE_OUT); // フェードアウト
        SoundManager.Instance.StopAll(); // 音を止める
        await UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        await fadeUtils.Fade(FadeUtils.E_FADE_TYPE.FADE_IN); // フェードイン
    }


    // シーンが存在するかどうかを確認するメソッド
    public bool IsSceneExist(string sceneName)
    {
        for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (sceneNameWithoutExtension == sceneName)
            {
                return true;
            }
        }
        return false;
    }
}
