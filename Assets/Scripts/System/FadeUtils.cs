using UnityEngine;
using Cysharp.Threading.Tasks;  // UniTaskを使用するためにインポート
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
    /// フェードの共通処理
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public async UniTask Fade(E_FADE_TYPE type)
    {
        isFade = true;  // フェード中フラグを立てる
        // フェードの開始と終了のアルファ値を設定
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

        // フェードの処理
        float timeElapsed = 0f;
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            alpha = Mathf.Lerp(startAlpha, endAlpha, timeElapsed / fadeDuration);


            await UniTask.Yield();  // 1フレーム待つ
        }

        // 最後のアルファ値を設定（精度を保つため）
        alpha = endAlpha;
        isFade = false;  // フェード終了フラグを下ろす
    }


}
