using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    [SerializeField]
    [Tooltip("�����ɂȂ炷���̐�")]
    int soundCount = 0; //���̐�

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
    /// ���g�p�̃I�[�f�B�I�\�[�X���擾���܂�
    /// </summary>
    /// <returns>
    /// true: ���g�p��AudioSource
    /// false: null
    /// </returns>
    private AudioSource GetUnusedAudioSource()
    {
        for (var i = 0; i < audioSourceList.Length; ++i)
        {
            if (audioSourceList[i].isPlaying == false) return audioSourceList[i];
        }

        return null; //���g�p��AudioSource�͌�����܂���ł���
    }

    /// <summary>
    /// �����̍Đ�
    /// </summary>
    /// <param name="clip"></param>
    void Play(in AudioClip clip, bool isLoop)
    {
        AudioSource audioSource = GetUnusedAudioSource();
        if (audioSource == null) return; //�Đ��ł��܂���ł���
        
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
