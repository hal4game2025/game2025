using UnityEngine;

public class HitAnim : MonoBehaviour
{
    [SerializeField, CustomLabel("�v���C���[")] protected Transform playerTrans;
    [HideInInspector] protected Animator animator;
    [SerializeField, CustomLabel("�q�b�g�A�j���ԍ�")] protected int[] animNum;
    [SerializeField, CustomLabel("�A�j���[�^�p�����^�[��")] protected string animName = "State";

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void PlayHitAnim()
    {
        animator.SetInteger(animName, animNum[0]);
    }
}
