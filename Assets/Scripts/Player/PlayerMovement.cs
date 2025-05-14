using NUnit.Framework.Internal.Execution;
using UnityEngine;


public interface IPlayerMovement
{
    /// <summary>
    /// ハンマーで飛ぶ
    /// </summary>
    /// <param name="inputDirection"></param>
    /// <param name="cameraRotation"></param>
    /// <param name="swingForce"></param>
    /// <param name="combo"></param>
    void SwingHammer(Vector2 inputDirection, in Transform camera, float swingForce, int combo);

    /// <summary>
    /// カメラの方向に向かって飛ぶ
    /// </summary>
    /// <param name="swingForce"></param>
    /// <param name="combo"></param>
    void SwingHammerMoveForward(Vector3 cameraForward, float swingForce, int combo);

    /// <summary>
    /// 入力方向を取得し、カメラの向きに合わせた方向を返す
    /// </summary>
    /// <param name="inputDirection"></param>
    /// <param name="cameraRotaion"></param>
    /// <returns></returns>
    Vector3 ReturnDirection(Vector2 inputDirection, Quaternion cameraRotaion);
    /// <summary>
    /// カメラの向きに合わせた方向を返す
    /// </summary>
    /// <param name="cameraRotation"></param>
    /// <returns></returns>
    Vector3 ReturnDirectionForward(Quaternion cameraRotation);
}

public class PlayerMovement : IPlayerMovement
{
    Rigidbody rb;
    float adjustSwingForce;
    public int max_coef = 0;
    private Vector3 hitStopBackupVelocity; // ヒットストップ用にバックアップする速度

    public PlayerMovement(Rigidbody _rb, float _adjustSwingForce)
        {
            rb = _rb;
            adjustSwingForce = _adjustSwingForce;
        }


    public Vector3 ReturnDirection(Vector2 inputDirection, Quaternion cameraRotaion)
    {
        // 入力がゼロの場合はゼロベクトルを返す
        if (inputDirection == Vector2.zero)
            return Vector3.zero;

        Vector3 forceDirection =  Vector3.Normalize(new Vector3(inputDirection.x, inputDirection.y,0.0f) );
        //カメラの向きに合わせる
        forceDirection = cameraRotaion * forceDirection;
        return forceDirection;
    }

    public Vector3 ReturnDirectionForward(Quaternion cameraRotation)
    {
        //カメラの向きに合わせる
        Vector3 forceDirection = cameraRotation * Vector3.forward;
        return forceDirection;
    }

    /// <summary>
    /// ヒットストップ開始　止まる前の速度を保存
    /// </summary>
    public void HitStopStart()
    {
        hitStopBackupVelocity = rb.linearVelocity;
        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;
    }
    /// <summary>
    /// ヒットストップ終了　保存した速度を復元
    /// </summary>
    public void HitStopEnd()
    {
        rb.isKinematic = false;
        rb.linearVelocity = hitStopBackupVelocity;
    }

        public void SwingHammer(Vector2 inputDirection, in Transform camera, float swingForce, int combo)
        {
            Vector3 forceDirection;
            
            if (inputDirection == Vector2.zero) // 入力がない場合は後ろに飛ぶ
                forceDirection = -camera.forward;
            else
            {
                forceDirection = Vector3.Normalize(new Vector3(inputDirection.x, inputDirection.y, 0.0f));
                forceDirection = camera.rotation * forceDirection;
            }

            //コンボ数で調整(要調整）
             float adjustedSwingForce = swingForce * Mathf.Sqrt(combo > max_coef ? max_coef : combo + 1) * adjustSwingForce;

            // 速度を直接設定
            rb.linearVelocity = forceDirection.normalized * adjustedSwingForce;
            Debug.Log(forceDirection.normalized * adjustedSwingForce);    
        }


        public void SwingHammerMoveForward(Vector3 cameraForward, float swingForce, int combo)
        {
            //コンボ数で調整(要調整）
            float adjustedSwingForce = swingForce * Mathf.Sqrt(combo > max_coef ? max_coef : combo + 1) * adjustSwingForce;
            // 速度を直接設定
            rb.linearVelocity = cameraForward * adjustedSwingForce;
        }

        

    }


