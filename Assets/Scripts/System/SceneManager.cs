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
        DontDestroyOnLoad(gameObject); // �V�[���J�ڎ��ɔj�����Ȃ��悤�ɐݒ�
        DontDestroyOnLoad(fadeImage.canvas); // �t�F�[�h�L�����o�X��j�����Ȃ��悤�ɐݒ�

        fadeUtils = new FadeUtils();
        fadeUtils.fadeDuration = 1.0f; // �t�F�[�h�̎��Ԃ�ݒ�
    }

    void Update()
    {
        // �t�F�[�h�̃A���t�@�l��UI�ɔ��f
        if (fadeUtils.IsFade)
        {
            fadeImage.color = new Color(0.0f,0.0f,0.0f,fadeUtils.Alpha);
        }
        
    }

    /// <summary>
    /// �V�[���J��
    /// </summary>
    /// <param name="sceneName"></param>
    async public void ChangeScene(string sceneName)
    {
        await fadeUtils.Fade(FadeUtils.E_FADE_TYPE.FADE_OUT); // �t�F�[�h�A�E�g
        SoundManager.Instance.StopAll(); // �����~�߂�
        await UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        await fadeUtils.Fade(FadeUtils.E_FADE_TYPE.FADE_IN); // �t�F�[�h�C��
    }
}
