using UnityEngine;
using UnityEngine.UIElements;

public class EnemyStateBase : MonoBehaviour
{
    public enum EnemyState
    {
        Idle = 0,   // 行動後の硬直に使うかも
        Chase,      // プレイヤーを追いかけるのと攻撃選択する
        Action,     // 行動処理
        Hit,        // よろける処理に使うかも
        Max
    }

    [SerializeField, CustomLabel("プレイヤーの座標")] Transform player;

    [SerializeField, CustomLabel("攻撃コライダー")] protected Collider[] atkCollider;
    protected EnemyStatus status;                       // ステータス
    protected Animator animator;                        // アニメーター
    protected EnemyState nowState = EnemyState.Chase;   // 現在のステート (最初から追いかける
    protected float delay = 0f;     // 行動後硬直時間
    protected float distance;       // プレイヤーとの距離

    static Vector3 contradictUP = new Vector3(1f, 0f, 1f);  // y軸の値打ち消し用

    void Start()
    {
        // ステータス取得
        status = GetComponent<EnemyStatus>();
        // アニメーター取得
        animator = GetComponent<Animator>();

        // 子クラスの初期化
        EnemyStart();
    }

    void Update()
    {
        // クールダウン処理
        CD();

        // プレイヤーとの距離
        distance = Vector3.Distance(player.position, transform.position);

        switch (nowState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Chase:
                Chase();        // 追いかける
                EnemyUpdate();  // 攻撃選択
                break;
            case EnemyState.Action:
                Action();   // 攻撃処理
                break;
            case EnemyState.Hit:
                Hit();
                break;
            default:
                Debug.Log("エネミーステートのswith外参照です");
                break;
        }
    }

    //--- public
    /// <summary>
    /// ステートを変更 (アニメーションイベントでも使用予定
    /// </summary>
    /// <param name="state"></param>
    public void SetEnemyState(int state)
    {
        // 例外処理
        if ((EnemyState)state > EnemyState.Max)
        {
            Debug.Log("EnemyStateBase 列挙外参照されました");
            return;
        }
        // ステート変更
        nowState = (EnemyState)state;
        // アニメーション
        animator.SetInteger("State", (int)nowState);
    }

    /// <summary>
    /// 攻撃用コライダーをアクティブ化
    /// </summary>
    /// <param name="atk"></param>
    public void ActiveCollider(int atk)
    {
        atkCollider[atk].enabled ^= true;
    }


    //--- private
    /// <summary>
    /// クールダウン処理
    /// </summary>
    private void CD()
    {
        for (int i = 0; i < status.atkDatas.Length; i++)
        {
            // 0以下はスルー
            if (status.atkDatas[i].nowCD <= 0f) continue;
            // クールダウン
            status.atkDatas[i].nowCD -= Time.deltaTime;
        }
    }

    /// <summary>
    /// 行動後硬直
    /// </summary>
    private void Idle()
    {
        if (delay <= 0f)
        {
            delay = 0f;
            // ステートをChaseに変更
            SetEnemyState((int)EnemyState.Chase);
            return;
        }

        // ディレイ処理
        delay -= Time.deltaTime;
        animator.SetInteger("Action", -1);
    }

    /// <summary>
    /// 追いかける処理
    /// </summary>
    private void Chase()
    {
        // プレイヤーの方を向く
        Vector3 pos = player.position - transform.position;                      // 方向ベクトル
        Quaternion rot = Quaternion.LookRotation(Vector3.Scale(pos, contradictUP)); // 方向を回転に変換
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, status.LookSpeed * Time.deltaTime);  // 適用

        // 最大接近距離じゃなかったら追いかける
        if (distance < status.Distance) return;

        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(player.position.x, transform.position.y, player.position.z),  // y軸は元のまま
            status.MoveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// よろけ処理
    /// </summary>
    private void Hit()
    {

    }


    //--- 子クラス
    /// <summary>
    /// 子クラスの初期化
    /// </summary>
    protected virtual void EnemyStart() { }
    /// <summary>
    /// 攻撃選択
    /// </summary>
    protected virtual void EnemyUpdate() { }
    /// <summary>
    /// 攻撃処理
    /// </summary>
    protected virtual void Action() { }
}
