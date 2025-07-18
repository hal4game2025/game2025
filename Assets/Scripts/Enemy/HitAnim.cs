using UnityEngine;

public class HitAnim : MonoBehaviour
{
    [SerializeField, CustomLabel("プレイヤー")] protected Transform playerTrans;
    [HideInInspector] protected Animator animator;
    [SerializeField, CustomLabel("ヒットアニメ番号")] protected int[] animNum;
    [SerializeField, CustomLabel("アニメータパラメター名")] protected string animName = "State";

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void PlayHitAnim()
    {
        animator.SetInteger(animName, animNum[0]);
    }
}
