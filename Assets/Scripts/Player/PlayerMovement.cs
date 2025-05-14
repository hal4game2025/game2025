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
    void SwingHammer(Vector2 inputDirection, in Transform camera, float swingForce, int combo);

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
    public int max_coef = 0;
    private Vector3 hitStopBackupVelocity; // �q�b�g�X�g�b�v�p�Ƀo�b�N�A�b�v���鑬�x

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

    /// <summary>
    /// �q�b�g�X�g�b�v�J�n�@�~�܂�O�̑��x��ۑ�
    /// </summary>
    public void HitStopStart()
    {
        hitStopBackupVelocity = rb.linearVelocity;
        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;
    }
    /// <summary>
    /// �q�b�g�X�g�b�v�I���@�ۑ��������x�𕜌�
    /// </summary>
    public void HitStopEnd()
    {
        rb.isKinematic = false;
        rb.linearVelocity = hitStopBackupVelocity;
    }

        public void SwingHammer(Vector2 inputDirection, in Transform camera, float swingForce, int combo)
        {
            Vector3 forceDirection;
            
            if (inputDirection == Vector2.zero) // ���͂��Ȃ��ꍇ�͌��ɔ��
                forceDirection = -camera.forward;
            else
            {
                forceDirection = Vector3.Normalize(new Vector3(inputDirection.x, inputDirection.y, 0.0f));
                forceDirection = camera.rotation * forceDirection;
            }

            //�R���{���Œ���(�v�����j
             float adjustedSwingForce = swingForce * Mathf.Sqrt(combo > max_coef ? max_coef : combo + 1) * adjustSwingForce;

            // ���x�𒼐ڐݒ�
            rb.linearVelocity = forceDirection.normalized * adjustedSwingForce;
            Debug.Log(forceDirection.normalized * adjustedSwingForce);    
        }


        public void SwingHammerMoveForward(Vector3 cameraForward, float swingForce, int combo)
        {
            //�R���{���Œ���(�v�����j
            float adjustedSwingForce = swingForce * Mathf.Sqrt(combo > max_coef ? max_coef : combo + 1) * adjustSwingForce;
            // ���x�𒼐ڐݒ�
            rb.linearVelocity = cameraForward * adjustedSwingForce;
        }

        

    }


