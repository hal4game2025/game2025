using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyStatus : MonoBehaviour
{
    // 行動情報構造体
    [System.Serializable] public struct AtkData
    {
        [CustomLabel("クールダウン(秒)")] public float maxCD;
        [CustomLabel("攻撃力")] public float damage;
        [CustomLabel("最大攻撃可能距離")] public float maxRange;
        [CustomLabel("最小攻撃可能距離")] public float minRange;
        [CustomLabel("行動後硬直時間")] public float delay;
        [HideInInspector] public float nowCD;   // 実際に使用するクールダウン
    }

    [SerializeField, CustomLabel("最大体力")]   float maxHP = 100f;
    [SerializeField, CustomLabel("現在の体力")] float nowHP = 100f;
    [SerializeField, CustomLabel("移動スピード")] float moveSpeed = 10f;
    [SerializeField, CustomLabel("振り向き速度")] float lookSpeed = 1f;
    [SerializeField, CustomLabel("最大接近距離")] float distance  = 30f;
    [CustomLabel("攻撃情報"), Tooltip("優先順位が高い行動から")] public AtkData[] atkDatas;
    [HideInInspector] public bool isAtk{ get; set; }

    public float MaxHP { get => maxHP; }
    public float HP { get => nowHP; }
    public float MoveSpeed { get => moveSpeed; }
    public float LookSpeed {  get => lookSpeed; }
    public float Distance { get => distance; }

    void Start()
    {
        nowHP = maxHP;      // 体力を最大にする
        isAtk = false;      // 攻撃中じゃない
        // クールダウン適用
        for (int i = 0; i < atkDatas.Length; ++i) atkDatas[i].nowCD = atkDatas[i].maxCD;
 
    }

    void Update()
    {
        // 仮で非表示
        if (nowHP <= 0f)
        { 
            gameObject.SetActive(false); // HPが０になったら非表示    
            SceneManager.Instance.ChangeScene("StageSelect");
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
