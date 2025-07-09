using UnityEngine;

public class AIController : MonoBehaviour
{
    /// <summary>
    /// アニメーションの状態
    /// </summary>
    public enum EnemyAnimState
    {
        None = 0,   // 無
        End,        // 終了
        Event,      // 特定の処理を挟みたい時
        Start,      // Eventまでの処理
        Next,       // 次のアニメーションがある場合
    }

    /// <summary>
    /// 敵AIの思考に必要なデータ
    /// </summary>
    [System.Serializable]
    public struct EnemyData
    {
        [HideInInspector] public EnemyStatus status;        // 敵ステータス
        [HideInInspector] public EnemyAnimState animState;  // アニメーションの状態
        [HideInInspector] public Animator anim;             // アニメーター
        public string animParamName;                        // アニメーションのパラメター名
        public int animIdel;                                // 待機モーションの数
        public BoxCollider[] boxColliders;                  // 攻撃コライダーまとめ

        /// <summary>
        /// アニメーションの状態を変える
        /// </summary>
        /// <param name="state"></param>
        public void SetEnemyAnimState(EnemyAnimState state)
        {
            animState = state;
        }

        /// <summary>
        /// 攻撃コライダーを有効化
        /// </summary>
        /// <param name="num"></param>
        public void OnBoxCollider(int num)
        {
            boxColliders[num].enabled = true;
        }

        /// <summary>
        /// 攻撃コライダーを無効化
        /// </summary>
        /// <param name="num"></param>
        public void OffBoxCollider(int num)
        {
            boxColliders[num].enabled = false;
        }
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
        data.animState = EnemyAnimState.None;
        data.anim = GetComponent<Animator>();       // アニメーター    

        //--- 例外チェック
        if (!bt) Debug.Log("AIController AIBehaviorTreeがない");
        if (!playerTransform) Debug.Log("AIController プレイヤーの座標がない");
        if (!data.status) Debug.Log("AIController 敵ステータスない");
        if (!data.anim) Debug.Log("AIController Animatorがない");

        // ビヘイビアツリーの初期化
        bt.BTInit();
    }

    void Update()
    {
        // BT更新
        bt.BTUpdate(data, playerTransform);
    }


    /// <summary>
    /// アニメーションイベントで使う用
    /// </summary>
    /// <param name="state"></param>
    public void SetEnemyAnimState(int state)
    {
        data.SetEnemyAnimState((EnemyAnimState)state);
    }

    /// <summary>
    /// アニメーションイベントで使う用(有効化)
    /// </summary>
    /// <param name="num"></param>
    public void OnBoxCollider(int num)
    {
        data.OnBoxCollider(num);
    }

    /// <summary>
    /// アニメーションイベントで使う用(無効化)
    /// </summary>
    /// <param name="num"></param>
    public void OffBoxCollider(int num)
    {
        data.OffBoxCollider(num);
    }
}
