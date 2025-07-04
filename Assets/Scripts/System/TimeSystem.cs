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
        //シーンがロードされたときに呼ばれるイベントに登録
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        //初期シーンのインデックスもセット
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
        //イベント解除
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //シーンがロードされたときに呼ばれる
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        m_sceneIndex = scene.buildIndex;
    }

    void Update()
    {

        //もしシーンの順番が変わったり、増えたらここを変更する必要がある
        switch (m_sceneIndex)
        {
            case 3: //ステージ１
            case 4: //ステージ２
                CountUp();
                break;
            default://その他
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
