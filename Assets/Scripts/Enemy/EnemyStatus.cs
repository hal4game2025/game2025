using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField, CustomLabel("ヒットアニメーションスクリプト")] HitAnim hitAnim;
    [HideInInspector] Animator animator;
    [SerializeField, CustomLabel("死亡アニメ番号")] int animNum = 0;
    [SerializeField, CustomLabel("最大体力")]   float maxHP = 100f;
    [SerializeField, CustomLabel("現在の体力")] float nowHP = 100f;
    [SerializeField, CustomLabel("振り向き速度")] float lookSpeed = 1f;

    [CustomLabel("硬直時間")] public float stiffnessTime = 0f;

    public float MaxHP { get => maxHP; }
    public float HP { get => nowHP; }
    public float LookSpeed { get => lookSpeed; }

    private bool sceneChangeFlg = false;
    void Start()
    {
        nowHP = maxHP-1;      // 体力を最大にする
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (nowHP <= 0f && !sceneChangeFlg)
        { 
            //gameObject.SetActive(false); // HPが０になったら非表示
            animator.SetInteger("State", animNum);
            SceneManager.Instance.ChangeScene("ResultScene");
            Debug.Log("HPが０になったので非表示");
            sceneChangeFlg = true;
        }
    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(float damage) 
    {
        if (nowHP <= 0f) return;
        nowHP -= damage;
        Debug.Log("ダメージを与えた: " + damage);

        // ヒットアニメーション
        //hitAnim.PlayHitAnim();
    }
        
}
