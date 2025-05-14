using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    /// <summary>
    /// 方向に基づき、アニメーションを設定する
    /// </summary>
    /// <param name="_direction"></param>
    public void SetAnimationByDirection(in Vector2 _direction)
    {
        if( _direction == null) { return; }

        if (_direction == Vector2.zero)
        {
            Debug.Log("アニメーション：前殴り");
        }
        else if (Mathf.Abs(_direction.y) > Mathf.Abs(_direction.x))
        {
            if (_direction.y > 0)
            {
                Debug.Log("アニメーション：下殴り");
            }
            else
            {
                Debug.Log("アニメーション：上殴り");
            }
        }
        else
        {
            
            if (_direction.x > 0)
            {
                Debug.Log("アニメーション：左殴り");

            }
            else
            {
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
}
