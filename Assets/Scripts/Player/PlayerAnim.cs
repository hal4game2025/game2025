using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    // 列挙
    /// <summary>
    /// アニメーションの状態
    /// </summary>
    public enum State
    {
        // 順番を変える場合はAnimator内の数値も変更すること
        Stand = 0,
        GroundFront,// 地面上で前殴り
        GroundBack, // 地面上で後殴り
        GroundUp,   // 地面上で上殴り
        GroundDown, // 地面上で下殴り
        GroundLeft, // 地面上で左殴り
        GroundRight,// 地面上で右殴り

        AirFront,   // 前殴り
        AirBack,    // 後殴り
        AirUp,      // 上殴り
        AirDown,    // 下殴り
        AirLeft,    // 左殴り
        AirRight,   // 右殴り

        AirPose,    // 空中姿勢
        Dorp,       // 落下中
        Landing,    // 着地
    }

    bool isLanging = false;

    Animator animator;
    Rigidbody rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        if (!animator) Debug.Log("PlayerAnim Animator取得失敗");
        if (!rb) Debug.Log("PlayerAnim Rigidbody取得失敗");
    }

    private void Update()
    {
        //if (rb.angularVelocity.y < -0.1f)
        
    }

    /// <summary>
    /// 方向に基づき、アニメーションを設定する
    /// </summary>
    /// <param name="_direction"></param>
    public void SetAnimationByDirection(in Vector2 _direction)
    {
        if( _direction == null) { return; }

        // 後
        if (_direction == Vector2.zero)
        {
            ChangeAnimation((int)State.AirFront);
        }
        else if (Mathf.Abs(_direction.y) > Mathf.Abs(_direction.x))
        {
            // 上下
            if (_direction.y > 0)
            {
                ChangeAnimation((int)State.AirDown);
            }
            else
            {
                ChangeAnimation((int)State.AirUp);  // 上
            }
        }
        else
        {
            // 左右
            if (_direction.x > 0)
            {
                ChangeAnimation((int)State.AirLeft);

            }
            else
            {
                ChangeAnimation((int)State.AirRight);
            }
        }
    }


    public void HitStopStart()
    {
        Debug.Log("アニメーション：ヒットストップ");
    }
    public void HitStopEnd()
    {
        Debug.Log("アニメーション：ヒットストップ終了");
    }

    /// <summary>
    /// カメラの向きに基づき、アニメーションを設定する
    /// </summary>
    public void SetAnimationByCameraForward()
    {
        ChangeAnimation((int)State.AirBack);   // 後殴り
    }

    /// <summary>
    /// アニメーションを変える
    /// </summary>
    /// <param name="num">変更先のアニメーション指定(アニメイベントに使う為int型)</param>
    public void ChangeAnimation(int num)
    {
        // アニメーション更新
        animator.SetInteger("State", num);
    }
}
