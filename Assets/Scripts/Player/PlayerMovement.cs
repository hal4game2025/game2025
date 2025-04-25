using NUnit.Framework.Internal.Execution;
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
    void SwingHammer(Vector2 inputDirection, Quaternion cameraRotation,float swingForce, int combo);

    /// <summary>
    /// �J�����̕����Ɍ������Ĕ��
    /// </summary>
    /// <param name="swingForce"></param>
    /// <param name="combo"></param>
    void SwingHammerMoveForward(Vector3 cameraForward, float swingForce, int combo);

    /// <summary>
    /// ���͕������擾���A�J�����̌����ɍ��킹��������Ԃ�
    /// </summary>
    /// <param name="inputDirection"></param>
    /// <param name="cameraRotaion"></param>
    /// <returns></returns>
    Vector3 ReturnDirection(Vector2 inputDirection, Quaternion cameraRotaion);
    /// <summary>
    /// �J�����̌����ɍ��킹��������Ԃ�
    /// </summary>
    /// <param name="cameraRotation"></param>
    /// <returns></returns>
    Vector3 ReturnDirectionForward(Quaternion cameraRotation);
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


    public Vector3 ReturnDirection(Vector2 inputDirection, Quaternion cameraRotaion)
    {
        // ���͂��[���̏ꍇ�̓[���x�N�g����Ԃ�
        if (inputDirection == Vector2.zero)
            return Vector3.zero;

        Vector3 forceDirection =  Vector3.Normalize(new Vector3(inputDirection.x, inputDirection.y,0.0f) );
        //�J�����̌����ɍ��킹��
        forceDirection = cameraRotaion * forceDirection;
        return forceDirection;
    }

    public Vector3 ReturnDirectionForward(Quaternion cameraRotation)
    {
        //�J�����̌����ɍ��킹��
        Vector3 forceDirection = cameraRotation * Vector3.forward;
        return forceDirection;
    }

    public void SwingHammer(Vector2 inputDirection, Quaternion cameraRotation,float swingForce, int combo)
        {
            Vector3 forceDirection;
            
            if (inputDirection == Vector2.zero) // ���͂��Ȃ��ꍇ�͌��ɔ��
                forceDirection = Vector3.back;
            else
                forceDirection = Vector3.Normalize(new Vector3(inputDirection.x, inputDirection.y, 0.0f));

            //�R���{���Œ���(�v�����j
            float adjustedSwingForce = swingForce * Mathf.Sqrt(combo + 1) * adjustSwingForce;

        //�v���C���[�̌����ɍ��킹��
       // Quaternion playerRotation = rb.transform.rotation;


        //�J�����̌����ɍ��킹��
        forceDirection = cameraRotation * forceDirection;

        // ���x�𒼐ڐݒ�
        rb.linearVelocity = forceDirection.normalized * adjustedSwingForce;
        }


        public void SwingHammerMoveForward(Vector3 cameraForward, float swingForce, int combo)
        {
            //�R���{���Œ���(�v�����j
            float adjustedSwingForce = swingForce * Mathf.Sqrt(combo + 1) * adjustSwingForce;
            // ���x�𒼐ڐݒ�
            rb.linearVelocity = cameraForward * adjustedSwingForce;
        }

        

    }


