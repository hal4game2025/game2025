using System;
using UnityEngine;
using TMPro;

public class TimeSystem : SingletonMonoBehaviour<TimeSystem>
{
    [SerializeField] private TextMeshProUGUI m_timeText;
    private float m_countUpSeconds; // �J�E���g�A�b�v�p
    private float m_time;

    private bool onlyOnesSound = false;
    private bool onlyOnesEndGameSound = false;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject); // ����GameObject���V�[���J�ڂŔj�����Ȃ�
        ResetTime();
    }
    void Update()
    {
        CountUp();
    }

    /// <summary>
    /// �J�E���g�A�b�v���s���֐�
    /// </summary>
    void CountUp()
    {
        m_time += Time.deltaTime;
        TimeSpan span = new TimeSpan(0, 0, (int)m_time);
        if (m_timeText != null)
            m_timeText.text = span.ToString(@"mm\:ss");
    }

    // �Q�[���N���A��Q�[���I�[�o�[���ɌĂяo���Ď��Ԃ��擾
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
