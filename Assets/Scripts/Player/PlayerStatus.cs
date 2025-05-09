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
    [SerializeField][Tooltip("空中で方向転換出来る最大数")] 
    int maxAirMove = 1;
    [SerializeField][Tooltip("障害物を殴ってから空中移動が可能になるまでの時間(s)")] 
    float airMoveCooldown = 0.5f;

    bool isStunned;
    bool isMuteki;
    int combo;
    int rate; // 攻撃の倍率
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

    public int Rate
    {
        get => rate;
        set
        {
            if(value >= 1)
            {
                rate = value;
            }
            else
            {
                rate = 1;
            }
        }
    }

    /// <summary>
    /// 空中移動が可能か？
    /// </summary>
    public bool CanAirMove => (airMoveCount < maxAirMove && airMoveCooldown < airMoveCooldownTimer);

    public void StunByObstacle()
    {
        if (combo > 0 && !isStunned && !isMuteki)
        {
            StartCoroutine(StunCoroutine());
        }

        RechargeAirMove();
    
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
    //public void ResetAirMoveCount() => airMoveCount = 0;

    /// <summary>
    /// 空中の移動回数を数える
    /// </summary>
    public void IncrementAirMoveCount() => airMoveCount++;

    /// <summary>
    /// 空中移動が可能になるまでの時間をリセットする
    /// </summary>
    //public void ResetAirMoveTimer() => airMoveCooldownTimer = 0.0f;

    
    /// <summary>
    /// 空中移動を再使用可能にする
    /// </summary>
    public void RechargeAirMove()
    {
        airMoveCount = 0;
        airMoveCooldownTimer = 0.0f;
    }

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