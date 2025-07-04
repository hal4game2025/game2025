using UnityEngine;

public class AIController : MonoBehaviour
{
    public enum EnemyAnimState
    {
        None = 0,   // 無
        End,        // 終了
        Event,      // 特定の処理を挟みたい時
    }

    /// <summary>
    /// 敵AIの思考に必要なデータ
    /// </summary>
    public struct EnemyData
    {
        [HideInInspector] public EnemyStatus status;    // 敵ステータス
        [HideInInspector] public EnemyAnimState state;  // アニメーションの状態
        [HideInInspector] public Animator anim;         // アニメーター
        // 攻撃コライダー(悩みポイント
    }

    [SerializeField, CustomLabel("ビヘイビアツリー"), Tooltip("ScriptableObjectをアタッチ")] 
    AIBehaviorTree bt;
    [SerializeField, CustomLabel("プレイヤーの座標")]
    Transform playerTransform;

    [CustomLabel("敵の情報")] public EnemyData data;

    void Start()
    {
        //--- データ取得
        data.status = GetComponent<EnemyStatus>();  // ステータス
        data.state = EnemyAnimState.None;
        data.anim = GetComponent<Animator>();       // アニメーター    

        //--- 例外チェック
        if (!bt) Debug.Log("AIController AIBehaviorTreeがない");
        if (!playerTransform) Debug.Log("AIController プレイヤーの座標がない");
        if (!data.status) Debug.Log("AIController 敵ステータスない");
        if (!data.anim) Debug.Log("AIController Animatorがない");

        // ビヘイビアツリーの初期化
        bt.BTInit(data, playerTransform);
    }

    void Update()
    {
        // BT更新
        bt.BTUpdate();
    }


    /// <summary>
    /// アニメーションの状態を変える
    /// </summary>
    /// <param name="state">アニメーションイベントでも使う為int型</param>
    public void SetEnemyAnimState(int state)
    {
        data.state = (EnemyAnimState)state;
    }
}
