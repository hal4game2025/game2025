using Unity.Mathematics;
using UnityEngine;

public interface IPlayerMovement
{
    /// <summary>
    /// �n���}�[�Ŕ��
    /// </summary>
    /// <param name="rotation">�v���C���[�̌���</param>
    /// <param name="inputDirection">���͕���</param>
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
    /// �n���}�[�Ŕ��
    /// </summary>
    /// <param name="rotation">�v���C���[�̌���</param>
    /// <param name="inputDirection">���͕���</param>
    public void SwingHammer(Vector2 inputDirection, float swingForce, int combo)
    {
        Vector3 forceDirection;
        if (inputDirection == Vector2.zero) // ���͂��Ȃ��ꍇ�͌��ɔ��
            forceDirection = Vector3.back;
        else
            forceDirection = Vector3.Normalize(new Vector3(inputDirection.x, inputDirection.y, 0.0f));
        //�R���{���Œ���
        float adjustedSwingForce =swingForce * Mathf.Sqrt(combo * adjustSwingForce);
        //�v���C���[�̌����ɍ��킹��
        Quaternion playerRotation = rb.transform.rotation;
        forceDirection = playerRotation * forceDirection;
        // ���x�𒼐ڐݒ�
        rb.linearVelocity = forceDirection * adjustedSwingForce;
    }


}
