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
        GroundDown, // 地面殴り
        AirFront,   // 前殴り
        // 後ろ殴り
        // 上殴り
        AirDown,    // 下殴り
        AirLeft,    // 左殴り
        AirRight,   // 右殴り
        AirPose,    // 空中姿勢
        Dorp,       // 落下中
        Landing,    // 着地
    }

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (!animator) Debug.Log("PlayerAnim Animator取得失敗");
    }

    /// <summary>
    /// 方向に基づき、アニメーションを設定する
    /// </summary>
    /// <param name="_direction"></param>
    public void SetAnimationByDirection(in Vector2 _direction)
    {
        if( _direction == null) { return; }

        if (_direction == Vector2.zero)
        {
            ChangeAnimation((int)State.AirFront);
            Debug.Log("アニメーション：前殴り");
        }
        else if (Mathf.Abs(_direction.y) > Mathf.Abs(_direction.x))
        {
            if (_direction.y > 0)
            {
                ChangeAnimation((int)State.AirDown);
                Debug.Log("アニメーション：下殴り");
            }
            else
            {
                //ChangeAnimation(State.);
                Debug.Log("アニメーション：上殴り");
            }
        }
        else
        {
            
            if (_direction.x > 0)
            {
                ChangeAnimation((int)State.AirLeft);
                Debug.Log("アニメーション：左殴り");

            }
            else
            {
                ChangeAnimation((int)State.AirRight);
                Debug.Log("アニメーション：右殴り");
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
        Debug.Log("アニメーション：カメラ視線方向移動");
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
