using Unity.Mathematics;
using UnityEngine;

public interface IPlayerMovement
{
    /// <summary>
    /// ハンマーで飛ぶ
    /// </summary>
    /// <param name="rotation">プレイヤーの向き</param>
    /// <param name="inputDirection">入力方向</param>
    void SwingHammer(Vector2 inputDirection, float swingForce, int combo);
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

    /// <summary>
    /// ハンマーで飛ぶ
    /// </summary>
    /// <param name="rotation">プレイヤーの向き</param>
    /// <param name="inputDirection">入力方向</param>
    public void SwingHammer(Vector2 inputDirection, float swingForce, int combo)
    {
        Vector3 forceDirection;
        if (inputDirection == Vector2.zero) // 入力がない場合は後ろに飛ぶ
            forceDirection = Vector3.back;
        else
            forceDirection = Vector3.Normalize(new Vector3(inputDirection.x, inputDirection.y, 0.0f));
        //コンボ数で調整
        float adjustedSwingForce =swingForce * Mathf.Sqrt(combo * adjustSwingForce);
        //プレイヤーの向きに合わせる
        Quaternion playerRotation = rb.transform.rotation;
        forceDirection = playerRotation * forceDirection;
        // 速度を直接設定
        rb.linearVelocity = forceDirection * adjustedSwingForce;
    }


}
