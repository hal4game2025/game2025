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
    [SerializeField] bool isNotStunned = false; //�e�X�g�p
    [SerializeField][Tooltip("�󒆂ŕ����]���o����ő吔")] 
    int maxAirMove = 1;
    [SerializeField][Tooltip("��Q���������Ă���󒆈ړ����\�ɂȂ�܂ł̎���(s)")] 
    float airMoveCooldown = 0.5f;

    bool isStunned;
    bool isMuteki;
    int combo;
    int rate; // �U���̔{��
    int airMoveCount;
    float airMoveCooldownTimer;

    public bool isFloor;  // ���ɐڒn���Ă��邩
    public bool IsStunned => isStunned;

    /// <summary>
    /// �_���[�W���󂯂�
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
                Debug.LogError("�R���{�����O��������[");
        }
    }

    public int Rate
    {
        get => rate;
        set
        {

            if(value < 0)
            {
                value = 1;
            }
            else if(value > 9999)
            {
                value = 9999;
            }

            rate = value;
        }
    }

    /// <summary>
    /// �󒆈ړ����\���H
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
            rate = 0;
            StartCoroutine(StunCoroutine());
            
        }
    }



    /// <summary>
    /// �󒆂̈ړ��񐔂𐔂���
    /// </summary>
    public void IncrementAirMoveCount() => airMoveCount++;

    /// <summary>
    /// �󒆈ړ����\�ɂȂ�܂ł̎��Ԃ����Z�b�g����
    /// </summary>
    //public void ResetAirMoveTimer() => airMoveCooldownTimer = 0.0f;

    
    /// <summary>
    /// �󒆈ړ����Ďg�p�\�ɂ���
    /// </summary>
    public void RechargeAirMove()
    {
        airMoveCount = 0;
        airMoveCooldownTimer = 0.0f;
    }

    /// <summary>
    /// �X�^����Ԃɂ���B�X�^����͈�莞�ԂނĂ�
    /// </summary>
    /// <returns></returns>
    private IEnumerator StunCoroutine()
    {
        //�e�X�g�p
        if (isNotStunned)
        {
            combo = 0;
            rate = 1;
            yield break;
        }
         

        Debug.Log("�X�^�����");
        combo = 0;
        rate = 1;
        isStunned = true;
        isMuteki = true;

        yield return new WaitForSeconds(stunDuration);
        isStunned = false;

        yield return new WaitForSeconds(mutekiDuration);
        isMuteki = false;
        Debug.Log("�X�^������");
    }


    private void OnDisable()
    {
        StopCoroutine(StunCoroutine());
        isStunned = false;

    }
    
}