using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    // ��
    /// <summary>
    /// �A�j���[�V�����̏��
    /// </summary>
    public enum State
    {
        // ���Ԃ�ς���ꍇ��Animator���̐��l���ύX���邱��
        Stand = 0,
        GroundDown, // �n�ʉ���
        AirFront,   // �O����
        // ��뉣��
        // �㉣��
        AirDown,    // ������
        AirLeft,    // ������
        AirRight,   // �E����
        AirPose,    // �󒆎p��
        Dorp,       // ������
        Landing,    // ���n
    }

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (!animator) Debug.Log("PlayerAnim Animator�擾���s");
    }

    /// <summary>
    /// �����Ɋ�Â��A�A�j���[�V������ݒ肷��
    /// </summary>
    /// <param name="_direction"></param>
    public void SetAnimationByDirection(in Vector2 _direction)
    {
        if( _direction == null) { return; }

        if (_direction == Vector2.zero)
        {
            ChangeAnimation((int)State.AirFront);
            Debug.Log("�A�j���[�V�����F�O����");
        }
        else if (Mathf.Abs(_direction.y) > Mathf.Abs(_direction.x))
        {
            if (_direction.y > 0)
            {
                ChangeAnimation((int)State.AirDown);
                Debug.Log("�A�j���[�V�����F������");
            }
            else
            {
                //ChangeAnimation(State.);
                Debug.Log("�A�j���[�V�����F�㉣��");
            }
        }
        else
        {
            
            if (_direction.x > 0)
            {
                ChangeAnimation((int)State.AirLeft);
                Debug.Log("�A�j���[�V�����F������");

            }
            else
            {
                ChangeAnimation((int)State.AirRight);
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

    /// <summary>
    /// �A�j���[�V������ς���
    /// </summary>
    /// <param name="num">�ύX��̃A�j���[�V�����w��(�A�j���C�x���g�Ɏg����int�^)</param>
    public void ChangeAnimation(int num)
    {
        // �A�j���[�V�����X�V
        animator.SetInteger("State", num);
    }
}
