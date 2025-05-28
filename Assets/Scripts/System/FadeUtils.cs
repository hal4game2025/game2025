using UnityEngine;
using Cysharp.Threading.Tasks;  // UniTask���g�p���邽�߂ɃC���|�[�g
public class FadeUtils
{
    public enum E_FADE_TYPE
    {
        FADE_IN,
        FADE_OUT,
    }

    
    public float fadeDuration = 1.0f;

    float alpha = 0.0f;
    public float Alpha
    {
        get { return alpha; }
    }


    bool isFade = false;
    public bool IsFade
    {
        get { return isFade; }
    }

    /// <summary>
    /// �t�F�[�h�̋��ʏ���
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public async UniTask Fade(E_FADE_TYPE type)
    {
        isFade = true;  // �t�F�[�h���t���O�𗧂Ă�
        // �t�F�[�h�̊J�n�ƏI���̃A���t�@�l��ݒ�
        float startAlpha = 0f;
        float endAlpha = 0f;

        switch (type)
        {
            case E_FADE_TYPE.FADE_IN:
                startAlpha = 1f;
                endAlpha = 0f;
                break;

            case E_FADE_TYPE.FADE_OUT:
                startAlpha = 0f;
                endAlpha = 1f;
                break;
        }

        // �t�F�[�h�̏���
        float timeElapsed = 0f;
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            alpha = Mathf.Lerp(startAlpha, endAlpha, timeElapsed / fadeDuration);


            await UniTask.Yield();  // 1�t���[���҂�
        }

        // �Ō�̃A���t�@�l��ݒ�i���x��ۂ��߁j
        alpha = endAlpha;
        isFade = false;  // �t�F�[�h�I���t���O�����낷
    }


}
