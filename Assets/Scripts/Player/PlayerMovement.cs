using UnityEngine;


public interface IPlayerMovement
{
    /// <summary>
    /// �n���}�[�Ŕ��
    /// </summary>
    /// <param name="inputDirection"></param>
    /// <param name="cameraRotation"></param>
    /// <param name="swingForce"></param>
    /// <param name="combo"></param>
    void SwingHammer(Vector2 inputDirection, Quaternion cameraRotation, float swingForce, int combo);

    /// <summary>
    /// �O�i���邾���̂��
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

        if (inputDirection == Vector2.zero) // ���͂��Ȃ��ꍇ�͌��ɔ��
            forceDirection = Vector3.back;
        else
            forceDirection = Vector3.Normalize(new Vector3(inputDirection.x, inputDirection.y, 0.0f));

        //�R���{���Œ���(�v�����j
        float adjustedSwingForce = swingForce * Mathf.Sqrt(combo + 1) * adjustSwingForce;

        //�v���C���[�̌����ɍ��킹��
        Quaternion playerRotation = rb.transform.rotation;
        forceDirection = playerRotation * forceDirection;
        // ���x�𒼐ڐݒ�
        rb.linearVelocity = forceDirection.normalized * adjustedSwingForce;
    }


    public void SwingHammerMoveForward(float swingForce, int combo)
    {
        //�R���{���Œ���(�v�����j
        float adjustedSwingForce = swingForce * Mathf.Sqrt(combo + 1) * adjustSwingForce;

        // ���x�𒼐ڐݒ�
        rb.linearVelocity = rb.transform.forward * adjustedSwingForce;
    }


}


