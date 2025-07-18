using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EnemyHPUI : MonoBehaviour
{
    [SerializeField] private EnemyStatus[] enemyStatus;  // HP�Q��
    [SerializeField] private Transform playerTransform;  // �v���C���[��Transform
    [SerializeField] private Transform gaugeParent;      // �Q�[�W����ׂ�e
    [SerializeField] private GameObject gaugePrefab;

    private List<Gauge> gauges = new List<Gauge>();

    [Header("HP�o�[")]
    [SerializeField] private Image hpBar;
    [SerializeField] private Image damageBar;

    [SerializeField] private float hpPerBar = 500f;

    private EnemyStatus nearestEnemy;

    void Start()
    {
        // �ŏ��͉����������Ȃ��i�G�����܂��Ă��琶���j
    }

    void Update()
    {
        nearestEnemy = GetNearestEnemy();
        if (nearestEnemy == null)
        {
            // ��\���ɂ������ꍇ
            hpBar.fillAmount = 0f;
            damageBar.fillAmount = 0f;
            foreach (var g in gauges) g.SetState(GaugeState.Black, 0f);
            return;
        }

        // �Q�[�W�����Ⴄ�ꍇ�͍�蒼��
        int gaugeCount = Mathf.CeilToInt(nearestEnemy.MaxHP / hpPerBar);
        if (gauges.Count != gaugeCount)
        {
            foreach (var g in gauges)
                Destroy(g.root);
            gauges.Clear();
            for (int i = 0; i < gaugeCount; i++)
            {
                GameObject gaugeObj = Instantiate(gaugePrefab, gaugeParent);
                Gauge gauge = new Gauge(gaugeObj);
                gauges.Add(gauge);
            }
        }

        UpdateHPBar();
        UpdateGauges();
    }

    /// <summary>
    /// �v���C���[��������Ƃ��߂��G�L�������擾����
    /// </summary>
    /// <returns></returns>
    private EnemyStatus GetNearestEnemy()
    {
        if (enemyStatus == null || enemyStatus.Length == 0 || playerTransform == null) return null;

        EnemyStatus nearest = null;
        float minSqrDist = float.MaxValue;
        foreach (var enemy in enemyStatus)
        {
            if (enemy == null || enemy.Deadflg) continue;
            float sqrDist = (enemy.transform.position - playerTransform.position).sqrMagnitude;
            if (sqrDist < minSqrDist)
            {
                minSqrDist = sqrDist;
                nearest = enemy;
            }
        }
        return nearest;
    }

    private void UpdateHPBar()
    {
        float remainingBar = (nearestEnemy.HP % hpPerBar) / hpPerBar;
        hpBar.fillAmount = remainingBar;
        damageBar.fillAmount = Mathf.Lerp(damageBar.fillAmount, hpBar.fillAmount, Time.deltaTime * 2f);
    }

    private void UpdateGauges()
    {
        int fullBars = Mathf.FloorToInt(nearestEnemy.HP / hpPerBar);
        float partial = nearestEnemy.HP % hpPerBar;

        for (int i = 0; i < gauges.Count; i++)
        {
            int reverseIndex = gauges.Count - 1 - i;

            if (i < fullBars)
            {
                gauges[reverseIndex].SetState(GaugeState.Green, 1f);
            }
            else if (i == fullBars)
            {
                if (partial > 0)
                {
                    gauges[reverseIndex].SetState(GaugeState.Red, partial / hpPerBar);
                }
                else if (nearestEnemy.HP > 0)
                {
                    gauges[reverseIndex].SetState(GaugeState.Green, 1f);
                }
                else
                {
                    gauges[reverseIndex].SetState(GaugeState.Black, 0f);
                }
            }
            else
            {
                gauges[reverseIndex].SetState(GaugeState.Black, 0f);
            }
        }
    }

    private class Gauge
    {
        public GameObject root;
        public Image greenPart;
        public Image redPart;

        public Gauge(GameObject root)
        {
            this.root = root;
            greenPart = root.transform.Find("Green").GetComponent<Image>();
            redPart = root.transform.Find("Red").GetComponent<Image>();
        }

        public void SetState(GaugeState state, float fillAmount)
        {
            switch (state)
            {
                case GaugeState.Green:
                    greenPart.gameObject.SetActive(true);
                    redPart.gameObject.SetActive(false);
                    greenPart.fillAmount = fillAmount;
                    break;
                case GaugeState.Red:
                    greenPart.gameObject.SetActive(false);
                    redPart.gameObject.SetActive(true);
                    redPart.fillAmount = fillAmount;
                    break;
                case GaugeState.Black:
                    greenPart.gameObject.SetActive(false);
                    redPart.gameObject.SetActive(false);
                    break;
            }
        }
    }

    private enum GaugeState
    {
        Green,
        Red,
        Black
    }
}
