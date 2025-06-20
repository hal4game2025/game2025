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
        GroundFront,// �n�ʏ�őO����
        GroundBack, // �n�ʏ�Ō㉣��
        GroundUp,   // �n�ʏ�ŏ㉣��
        GroundDown, // �n�ʏ�ŉ�����
        GroundLeft, // �n�ʏ�ō�����
        GroundRight,// �n�ʏ�ŉE����

        AirFront,   // �O����
        AirBack,    // �㉣��
        AirUp,      // �㉣��
        AirDown,    // ������
        AirLeft,    // ������
        AirRight,   // �E����

        AirPose,    // �󒆎p��
        Dorp,       // ������
        Landing,    // ���n
    }

    [SerializeField, Tooltip("��葬�x�ȉ��ɂȂ����痎���A�j���Đ�")]
    float minVelocity = 0.1f;
    [SerializeField] float aa;

    PlayerStatus status;
    Animator animator;
    Rigidbody rb;

    private void Start()
    {
        status = GetComponent<PlayerStatus>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        if (!status) Debug.Log("PlayerAnim PlayerStatus�擾���s");
        if (!animator) Debug.Log("PlayerAnim Animator�擾���s");
        if (!rb) Debug.Log("PlayerAnim Rigidbody�擾���s");
    }

    private void Update()
    {
        aa = rb.linearVelocity.sqrMagnitude;

        if (rb.linearVelocity.sqrMagnitude < minVelocity)
        {
            if (status.isFloor) ChangeAnimation((int)State.Landing);
            else ChangeAnimation((int)State.Dorp);
        }
    }

    /// <summary>
    /// �����Ɋ�Â��A�A�j���[�V������ݒ肷��
    /// </summary>
    /// <param name="_direction"></param>
    public void SetAnimationByDirection(in Vector2 _direction)
    {
        if( _direction == null) { return; }

        // ��
        if (_direction == Vector2.zero)
        {
            ChangeAnimation((int)State.AirFront);
        }
        else if (Mathf.Abs(_direction.y) > Mathf.Abs(_direction.x))
        {
            // �㉺
            if (_direction.y > 0)
            {
                ChangeAnimation((int)State.AirDown);
            }
            else
            {
                ChangeAnimation((int)State.AirUp);  // ��
            }
        }
        else
        {
            // ���E
            if (_direction.x > 0)
            {
                ChangeAnimation((int)State.AirLeft);

            }
            else
            {
                ChangeAnimation((int)State.AirRight);
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
        ChangeAnimation((int)State.AirBack);   // �㉣��
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
