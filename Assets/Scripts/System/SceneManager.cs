using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneManager : SingletonMonoBehaviour<SceneManager>
{
    [SerializeField]
    Image fadeImage;

    [SerializeField]
    GameObject imagePrefab; // image���Ȃ������Ƃ��̐����p

    FadeUtils fadeUtils;

    void Start()
    {
        if(fadeImage  == null)
        {
            
            fadeImage = Instantiate(imagePrefab).GetComponentInChildren<Image>();
        }

         

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

        if(!IsSceneExist(name))
        {
            Debug.LogError($"�V�[�� {sceneName} �͑��݂��܂���B");
            return;
        }

        await fadeUtils.Fade(FadeUtils.E_FADE_TYPE.FADE_OUT); // �t�F�[�h�A�E�g
        SoundManager.Instance.StopAll(); // �����~�߂�
        await UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        await fadeUtils.Fade(FadeUtils.E_FADE_TYPE.FADE_IN); // �t�F�[�h�C��
    }


    // �V�[�������݂��邩�ǂ������m�F���郁�\�b�h
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
