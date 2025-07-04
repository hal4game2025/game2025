using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeSystem : SingletonMonoBehaviour<TimeSystem>
{
    private float m_time;
    private int m_sceneIndex = 0;

    void Start()
    {
        m_time = 0f;
        //�V�[�������[�h���ꂽ�Ƃ��ɌĂ΂��C�x���g�ɓo�^
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        //�����V�[���̃C���f�b�N�X���Z�b�g
        m_sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
        ResetTime();
    }

    void OnDestroy()
    {
        //�C�x���g����
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //�V�[�������[�h���ꂽ�Ƃ��ɌĂ΂��
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        m_sceneIndex = scene.buildIndex;
    }

    void Update()
    {

        //�����V�[���̏��Ԃ��ς������A�������炱����ύX����K�v������
        switch (m_sceneIndex)
        {
            case 3: //�X�e�[�W�P
            case 4: //�X�e�[�W�Q
                CountUp();
                break;
            default://���̑�
                break;
        }
    }

    void CountUp()
    {
        m_time += Time.deltaTime;
    }

    public float GetTime()
    {
        return m_time;
    }

    public void ResetTime()
    {
        m_time = 0f;
    }
}
