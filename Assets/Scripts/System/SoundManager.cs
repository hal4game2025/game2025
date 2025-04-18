using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    [SerializeField]
    [Tooltip("同時にならす音の数")]
    int soundCount = 0; //音の数

    private AudioSource[] audioSourceList;


    private void Awake()
    {
        audioSourceList = new AudioSource[soundCount];

        for (int i = 0; i < soundCount; i++)
        {
            audioSourceList[i] = gameObject.AddComponent<AudioSource>();
        }
    }

    /// <summary>
    /// 未使用のオーディオソースを取得します
    /// </summary>
    /// <returns>
    /// true: 未使用のAudioSource
    /// false: null
    /// </returns>
    private AudioSource GetUnusedAudioSource()
    {
        for (var i = 0; i < audioSourceList.Length; ++i)
        {
            if (audioSourceList[i].isPlaying == false) return audioSourceList[i];
        }

        return null; //未使用のAudioSourceは見つかりませんでした
    }

    /// <summary>
    /// 音声の再生
    /// </summary>
    /// <param name="clip"></param>
    void Play(in AudioClip clip, bool isLoop)
    {
        AudioSource audioSource = GetUnusedAudioSource();
        if (audioSource == null) return; //再生できませんでした
        
        if(isLoop)
        {
            audioSource.clip = clip;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = clip;
            audioSource.loop = false;
            audioSource.Play();
        }

    }
}
