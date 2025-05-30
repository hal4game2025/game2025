using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneManager : SingletonMonoBehaviour<SceneManager>
{
    [SerializeField]
    Image fadeImage;

    FadeUtils fadeUtils;

    void Start()
    {
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
        await fadeUtils.Fade(FadeUtils.E_FADE_TYPE.FADE_OUT); // フェードアウト
        SoundManager.Instance.StopAll(); // 音を止める
        await UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        await fadeUtils.Fade(FadeUtils.E_FADE_TYPE.FADE_IN); // フェードイン
    }
}
