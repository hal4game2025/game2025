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
    void SwingHammer(Vector2 inputDirection, Quaternion cameraRotation, float swingForce, int combo);

    /// <summary>
    /// 前進するだけのやつ
    /// </summary>
    /// <param name="swingForce"></param>
    /// <param name="combo"></param>
    void SwingHammerMoveForward(float swingForce, int combo);
}

public class PlayerMovement : IPlayerMovement
{
    Rigidbody rb;
    float adjustSwingForce;
    public PlayerMovement(Rigidbody _rb, float _adjustSwingForce)
    {
        rb = _rb;
        adjustSwingForce = _adjustSwingForce;
    }


    public void SwingHammer(Vector2 inputDirection, Quaternion cameraRotation, float swingForce, int combo)
    {
        Vector3 forceDirection;

        if (inputDirection == Vector2.zero) // 入力がない場合は後ろに飛ぶ
            forceDirection = Vector3.back;
        else
            forceDirection = Vector3.Normalize(new Vector3(inputDirection.x, inputDirection.y, 0.0f));

        //コンボ数で調整(要調整）
        float adjustedSwingForce = swingForce * Mathf.Sqrt(combo + 1) * adjustSwingForce;

        //プレイヤーの向きに合わせる
        Quaternion playerRotation = rb.transform.rotation;
        forceDirection = playerRotation * forceDirection;
        // 速度を直接設定
        rb.linearVelocity = forceDirection.normalized * adjustedSwingForce;
    }


    public void SwingHammerMoveForward(float swingForce, int combo)
    {
        //コンボ数で調整(要調整）
        float adjustedSwingForce = swingForce * Mathf.Sqrt(combo + 1) * adjustSwingForce;

        // 速度を直接設定
        rb.linearVelocity = rb.transform.forward * adjustedSwingForce;
    }


}


