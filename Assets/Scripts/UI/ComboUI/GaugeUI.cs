using UnityEngine;
using UnityEngine.UI;
public class GaugeUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private Image gaugeUI;
    [SerializeField] PlayerStatus playerStatus;     //ƒRƒ“ƒ{Žæ“¾—p
    private float max = 9999;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gaugeUI.fillAmount = (float)playerStatus.Rate / max;
    }
}
