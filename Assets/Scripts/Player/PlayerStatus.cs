using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System.Runtime.InteropServices.WindowsRuntime;

public class PlayerStatus : MonoBehaviour
{

    [SerializeField] int hp = 10;
    public int HP { 
        get { return hp; } 
        private set { hp = value; }
    }

    [SerializeField] float stunDuration = 1.0f;
    [SerializeField] float mutekiDuration = 2;
    [SerializeField] bool isNotStunned = false; //テスト用
    [SerializeField] int maxAirMove = 1; // 空中で方向転換出来る最大数
    [SerializeField] float airMoveCooldown = 0.5f; // 空中移動が可能になるまでの時間

    bool isStunned;
    bool isMuteki;
    int combo;
    int airMoveCount;
    float airMoveCooldownTimer;

    public bool IsStunned => isStunned;

    /// <summary>
    /// ダメージを受ける
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage)
    {
        HP -= damage;
    }

    public int AirMoveCount => airMoveCount;

    private void Start()
    {
        isStunned = false;
        isMuteki = false;
        combo = 0;
        airMoveCooldown = 0;
        airMoveCooldownTimer = 0.0f;
    }

    private void Update()
    {
        airMoveCooldownTimer += Time.deltaTime;
    }

    public int Combo
    {
        get => combo;
        set
        {
            if (value > 0)
                combo = value;
            else
                Debug.LogError("コンボ数が０未満だよー");
        }
    }

    /// <summary>
    /// 空中移動が可能か？
    /// </summary>
    public bool CanAirMove => (airMoveCount < maxAirMove && airMoveCooldown < airMoveCooldownTimer);

    /// <summary>
    /// 空中移動はクールタイム中か？
    /// </summary>
    public bool HasAirMoveCooldown => (airMoveCooldown < airMoveCooldownTimer);

    public void StunByObstacle()
    {
        if (combo > 0 && !isStunned && !isMuteki)
        {
            StartCoroutine(StunCoroutine());
        }
    }

    public void StunByEnemy()
    {
        if (!isStunned && !isMuteki)
        {
            StartCoroutine(StunCoroutine());
            
        }
    }

    /// <summary>
    /// 空中の移動回数をリセットする
    /// </summary>
    public void ResetAirJumpCount() => airMoveCount = 0;

    /// <summary>
    /// 空中の移動回数を数える
    /// </summary>
    public void IncrementAirJumpCount() => airMoveCount++;

    /// <summary>
    /// 空中移動が可能になるまでの時間をリセットする
    /// </summary>
    public void ResetAirMoveTimer() => airMoveCooldownTimer = 0.0f;

    /// <summary>
    /// スタン状態にする。スタン後は一定時間むてき
    /// </summary>
    /// <returns></returns>
    private IEnumerator StunCoroutine()
    {
        //テスト用
        if (isNotStunned)
        {
            combo = 0;
            yield break;
        }
         

        Debug.Log("スタン状態");
        combo = 0;
        isStunned = true;
        isMuteki = true;

        yield return new WaitForSeconds(stunDuration);
        isStunned = false;

        yield return new WaitForSeconds(mutekiDuration);
        isMuteki = false;
        Debug.Log("スタン解除");
    }
}