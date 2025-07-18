using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField, CustomLabel("最大体力")]   float maxHP = 100f;
    [SerializeField, CustomLabel("現在の体力")] float nowHP = 100f;
    [SerializeField, CustomLabel("振り向き速度")] float lookSpeed = 1f;

    [CustomLabel("硬直時間")] public float stiffnessTime = 0f;

    public float MaxHP { get => maxHP; }
    public float HP { get => nowHP; }
    public float LookSpeed { get => lookSpeed; }

    void Start()
    {
        nowHP = maxHP-1;      // 体力を最大にする
    }                         // HPUIでどうしようもないバグがあるため-1させていただいてます

    void Update()
    {
        if (nowHP <= 0f)
        { 
            gameObject.SetActive(false); // HPが０になったら非表示    
            SceneManager.Instance.ChangeScene("ResultScene");
            Debug.Log("HPが０になったので非表示");
        }
    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(float damage) 
    {
        if (nowHP <= 0f) return;
        nowHP -= damage;
        Debug.Log("ダメージを与えた: " + damage);
    }
        
}
