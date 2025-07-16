using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EnemyHPUI : MonoBehaviour
{
    [SerializeField] private EnemyStatus enemyStatus;  // HP参照
    [SerializeField] private Transform gaugeParent;    // ゲージを並べる親
    [SerializeField] private GameObject gaugePrefab;

    private List<Gauge> gauges = new List<Gauge>();

    [Header("HPバー")]
    [SerializeField] private Image hpBar;
    [SerializeField] private Image damageBar;

    [SerializeField] private float hpPerBar = 500f;

    void Start()
    {

        CreateGauges();
    }

    void Update()
    {
        UpdateHPBar();
        UpdateGauges();
    }

    private void CreateGauges()
    {
        // MaxHPに応じて必要なゲージ数を作成
        int gaugeCount = Mathf.CeilToInt(enemyStatus.MaxHP / hpPerBar);
        for (int i = 0; i < gaugeCount; i++)
        {
            GameObject gaugeObj = Instantiate(gaugePrefab, gaugeParent);
            Gauge gauge = new Gauge(gaugeObj);
            gauges.Add(gauge);
        }
    }

    private void UpdateHPBar()
    {
        float remainingBar = (enemyStatus.HP % hpPerBar) / hpPerBar;
        hpBar.fillAmount = remainingBar;

        damageBar.fillAmount = Mathf.Lerp(damageBar.fillAmount, hpBar.fillAmount, Time.deltaTime * 2f);
    }

    private void UpdateGauges()
    {
        int fullBars = Mathf.FloorToInt(enemyStatus.HP / hpPerBar);
        float partial = enemyStatus.HP % hpPerBar;

        for (int i = 0; i < gauges.Count; i++)
        {
            int reverseIndex = gauges.Count - 1 - i;

            if (i < fullBars)
            {
                // フルバー
                gauges[reverseIndex].SetState(GaugeState.Green, 1f);
            }
            else if (i == fullBars)
            {
                if (partial > 0)
                {
                    gauges[reverseIndex].SetState(GaugeState.Red, partial / hpPerBar);
                }
                else if (enemyStatus.HP > 0)
                {
                    // 端数が0でも HP が残ってるなら緑
                    gauges[reverseIndex].SetState(GaugeState.Green, 1f);
                }
                else
                {
                    gauges[reverseIndex].SetState(GaugeState.Black, 0f);
                }
            }
            else
            {
                // フルにも端数にも含まれない → 空
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

        // fillAmount を受け取れるように変更
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
