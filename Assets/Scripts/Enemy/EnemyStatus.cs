using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    // 攻撃情報構造体
    [System.Serializable] public struct AtkData
    {
        [CustomLabel("クールダウン(秒)")] public float maxCD;
        [CustomLabel("攻撃力")] public float atkDamage;
        [CustomLabel("攻撃可能距離")] public float atkRange;
        [HideInInspector] public float nowCD;
    }

    [SerializeField, CustomLabel("最大体力")]   float maxHP = 100f;
    [SerializeField, CustomLabel("現在の体力")] float nowHP = 100f;
    [SerializeField, CustomLabel("移動スピード")] float moveSpeed = 10f;
    [SerializeField, CustomLabel("振り向き速度")] float lookSpeed = 1f;
    [SerializeField, CustomLabel("最大接近距離")] float distance  = 20f;
    [SerializeField, CustomLabel("攻撃情報")] public AtkData[] atkData;
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
        for (int i = 0; i < atkData.Length; ++i) atkData[i].nowCD = atkData[i].maxCD;
    }

    void Update()
    {
        // 仮で非表示
        if (nowHP <= 0f) gameObject.SetActive(false);
    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(float damage) 
    {
        if (nowHP <= 0f) return;
        nowHP -= damage;
    }
        
}
