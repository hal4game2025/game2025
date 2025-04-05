using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class PlayerStatus : MonoBehaviour
{

    [SerializeField] float stunDuration = 1.0f;
    [SerializeField] float mutekiDuration = 2;
    [SerializeField] bool isNotStunned = false; //テスト用
    bool isStunned;
    bool isMuteki;
    int combo;
    public bool IsStunned => isStunned;
    private void Start()
    {
        isStunned = false;
        isMuteki = false;
        combo = 0;
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