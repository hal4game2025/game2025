using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    /// <summary>
    /// �����Ɋ�Â��A�A�j���[�V������ݒ肷��
    /// </summary>
    /// <param name="_direction"></param>
    public void SetAnimationByDirection(in Vector2 _direction)
    {
        if( _direction == null) { return; }

        if (_direction == Vector2.zero)
        {
            Debug.Log("�A�j���[�V�����F�O����");
        }
        else if (Mathf.Abs(_direction.y) > Mathf.Abs(_direction.x))
        {
            if (_direction.y > 0)
            {
                Debug.Log("�A�j���[�V�����F������");
            }
            else
            {
                Debug.Log("�A�j���[�V�����F�㉣��");
            }
        }
        else
        {
            
            if (_direction.x > 0)
            {
                Debug.Log("�A�j���[�V�����F������");

            }
            else
            {
                Debug.Log("�A�j���[�V�����F�E����");
            }
        }
    }
    public void HitStopStart()
    {
        Debug.Log("�A�j���[�V�����F�q�b�g�X�g�b�v");
    }
    public void HitStopEnd()
    {
        Debug.Log("�A�j���[�V�����F�q�b�g�X�g�b�v�I��");
    }


    /// <summary>
    /// �J�����̌����Ɋ�Â��A�A�j���[�V������ݒ肷��
    /// </summary>
    public void SetAnimationByCameraForward()
    {   
        Debug.Log("�A�j���[�V�����F�J�������������ړ�");
    }
}
