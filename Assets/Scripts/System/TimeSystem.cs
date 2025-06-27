using System;
using UnityEngine;
using TMPro;

public class TimeSystem : SingletonMonoBehaviour<TimeSystem>
{
    [SerializeField] private TextMeshProUGUI m_timeText;
    private float m_countUpSeconds; // カウントアップ用
    private float m_time;

    private bool onlyOnesSound = false;
    private bool onlyOnesEndGameSound = false;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject); // このGameObjectをシーン遷移で破棄しない
        ResetTime();
    }
    void Update()
    {
        CountUp();
    }

    /// <summary>
    /// カウントアップを行う関数
    /// </summary>
    void CountUp()
    {
        m_time += Time.deltaTime;
        TimeSpan span = new TimeSpan(0, 0, (int)m_time);
        if (m_timeText != null)
            m_timeText.text = span.ToString(@"mm\:ss");
    }

    // ゲームクリアやゲームオーバー時に呼び出して時間を取得
    public float GetTime()
    {
        return m_time;
    }

    public void ResetTime()
    {
        m_time = 0f;
        if (m_timeText != null)
            m_timeText.text = "00:00";
    }
}
