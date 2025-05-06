using UnityEngine;
using UnityEngine.UIElements;

public class EnemyStateBase : MonoBehaviour
{
    public enum EnemyState
    {
        Idle = 0,   // 攻撃後の硬直に使うかも
        Chase,      // プレイヤーを追いかけるのと攻撃選択する
        Attack,     // 攻撃処理
        Hit,        // よろける処理に使うかも
        Max
    }

    [SerializeField, CustomLabel("プレイヤーの座標")] Transform playerPos;

    protected EnemyStatus status;                       // ステータス
    protected EnemyState nowState = EnemyState.Chase;   // 現在のステート (最初から追いかける
    protected float playerDistance;                     // プレイヤーとの距離

    static Vector3 contradictUP = new Vector3(1f, 0f, 1f);  // y軸の値打ち消し用

    void Start()
    {
        // ステータス取得
        status = GetComponent<EnemyStatus>();
        // プレイヤーとの距離
        playerDistance = Vector3.Distance(playerPos.position, transform.position);

        // 子クラスの初期化
        EnemyStart();
    }

    void Update()
    {
        // クールダウン処理
        CD();

        // プレイヤーとの距離
        playerDistance = Vector3.Distance(playerPos.position, transform.position);

        switch (nowState)
        {
            case EnemyState.Idle:
                break;
            case EnemyState.Chase:
                Chase();        // 追いかける
                EnemyUpdate();  // 攻撃選択

                break;
            case EnemyState.Attack:
                Attack();   // 攻撃処理

                break;
            case EnemyState.Hit:
                break;
            default:
                Debug.Log("エネミーステートのswith外参照です");
                break;
        }
    }


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
    }


    //--- private
    /// <summary>
    /// クールダウン処理
    /// </summary>
    private void CD()
    {
        for (int i = 0; i < status.atkData.Length; i++)
        {
            // 0以下はスルー
            if (status.atkData[i].nowCD <= 0f) continue;
            // クールダウン
            status.atkData[i].nowCD -= Time.deltaTime;
        }
    }

    /// <summary>
    /// 追いかける処理
    /// </summary>
    private void Chase()
    {
        // プレイヤーの方を向く
        Vector3 pos = playerPos.position - transform.position;                      // 方向ベクトル
        Quaternion rot = Quaternion.LookRotation(Vector3.Scale(pos, contradictUP)); // 方向を回転に変換
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, status.LookSpeed * Time.deltaTime);  // 適用

        // 最大接近距離じゃなかったら追いかける
        if (playerDistance < status.Distance) return;

        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(playerPos.position.x, transform.position.y, playerPos.position.z),  // y軸は元のまま
            status.MoveSpeed * Time.deltaTime);
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
    protected virtual void Attack() { }
}
