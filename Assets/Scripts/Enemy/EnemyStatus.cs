using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField, CustomLabel("�q�b�g�A�j���[�V�����X�N���v�g")] HitAnim hitAnim;
    [HideInInspector] Animator animator;
    [SerializeField, CustomLabel("���S�A�j���ԍ�")] int animNum = 0;
    [SerializeField, CustomLabel("�ő�̗�")]   float maxHP = 100f;
    [SerializeField, CustomLabel("���݂̗̑�")] float nowHP = 100f;
    [SerializeField, CustomLabel("�U��������x")] float lookSpeed = 1f;

    [CustomLabel("�d������")] public float stiffnessTime = 0f;

    public float MaxHP { get => maxHP; }
    public float HP { get => nowHP; }
    public float LookSpeed { get => lookSpeed; }

    private bool sceneChangeFlg = false;
    void Start()
    {
        nowHP = maxHP-1;      // �̗͂��ő�ɂ���
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (nowHP <= 0f && !sceneChangeFlg)
        { 
            //gameObject.SetActive(false); // HP���O�ɂȂ������\��
            animator.SetInteger("State", animNum);
            SceneManager.Instance.ChangeScene("ResultScene");
            Debug.Log("HP���O�ɂȂ����̂Ŕ�\��");
            sceneChangeFlg = true;
        }
    }

    /// <summary>
    /// �_���[�W����
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(float damage) 
    {
        if (nowHP <= 0f) return;
        nowHP -= damage;
        Debug.Log("�_���[�W��^����: " + damage);

        // �q�b�g�A�j���[�V����
        //hitAnim.PlayHitAnim();
    }
        
}
