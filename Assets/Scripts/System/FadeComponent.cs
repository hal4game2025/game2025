using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeComponent : MonoBehaviour 
{
    public enum E_STATE
    {
        FADE_IN = 0,
        FADE_OUT = 1,
        FADE_MAX
    }

    [SerializeField]
    RawImage rawImage;

    public bool isFade { get; private set; }


    public float fadeOutRate { get; set; }

    public float fadeInRate { get; set; }
   
    public float fadeDelay { get; set; }


    E_STATE status = E_STATE.FADE_MAX;

    public float alpha { get; private set; }

    void Start()
    {
        alpha = rawImage.color.a;
        isFade = false;
    }

    void Update()
    {
        if (!isFade) return;

        switch (status)
        {
            case E_STATE.FADE_IN:
                if (alpha >= 1.0f)
                {
                    isFade = false;
                }
                break;

            case E_STATE.FADE_OUT:
                if (alpha <= 0.0f)
                {
                    isFade = false;
                }
                break;
        }

    }

    /// <summary>
    /// フェードアウト→フェードインの順で行う
    /// </summary>
    /// <param name="fadeTime">フェードにかかる時間</param>
    /// <returns></returns>
    public IEnumerator StartFadeOutIn(float fadeTime)
    {
       if(!isFade) 
       {
            status = E_STATE.FADE_OUT;
            yield return Fade(1.0f,0.0f, fadeTime / fadeOutRate);
            yield return new WaitForSeconds(fadeDelay);

            status = E_STATE.FADE_IN;
            yield return Fade(0.0f,1.0f, fadeTime / fadeInRate);
       }   
    }

    /// <summary>
    /// フェードを行う
    /// </summary>
    /// <param name="state">フェードの種類</param>
    /// <param name="fadeTime">フェードにかかる時間</param>
    /// <returns></returns>
    public IEnumerator StartFade(E_STATE state, float fadeTime)
    {
        if (!isFade)
        {
            status = state;

            float START_ALPHA = state == E_STATE.FADE_IN ? 0.0f : 1.0f;
            float END_ALPHA = state == E_STATE.FADE_IN ? 1.0f : 0.0f;


            yield return Fade(START_ALPHA, END_ALPHA, fadeTime);
        }
    }

    // --------------- サブルーチン ------------------------------------
    IEnumerator Fade(float START_ALPHA,float END_ALPHA, float fadeTime)
    {
        float time = 0.0f;
        isFade = true;

        while (time < fadeTime)
        {
            time += Time.deltaTime;
            alpha = Mathf.Lerp(START_ALPHA, END_ALPHA, time / fadeTime);

            rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, alpha);

            yield return null;
            
        }

        alpha = END_ALPHA;
    }


    
}
