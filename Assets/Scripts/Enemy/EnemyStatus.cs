using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyStatus : MonoBehaviour
{
    // �s�����\����
    [System.Serializable] public struct AtkData
    {
        [CustomLabel("�N�[���_�E��(�b)")] public float maxCD;
        [CustomLabel("�U����")] public float damage;
        [CustomLabel("�ő�U���\����")] public float maxRange;
        [CustomLabel("�ŏ��U���\����")] public float minRange;
        [CustomLabel("�s����d������")] public float delay;
        [HideInInspector] public float nowCD;   // ���ۂɎg�p����N�[���_�E��
    }

    [SerializeField, CustomLabel("�ő�̗�")]   float maxHP = 100f;
    [SerializeField, CustomLabel("���݂̗̑�")] float nowHP = 100f;
    [SerializeField, CustomLabel("�ړ��X�s�[�h")] float moveSpeed = 10f;
    [SerializeField, CustomLabel("�U��������x")] float lookSpeed = 1f;
    [SerializeField, CustomLabel("�ő�ڋߋ���")] float distance  = 30f;
    [CustomLabel("�U�����"), Tooltip("�D�揇�ʂ������s������")] public AtkData[] atkDatas;
    [HideInInspector] public bool isAtk{ get; set; }

    public float MaxHP { get => maxHP; }
    public float HP { get => nowHP; }
    public float MoveSpeed { get => moveSpeed; }
    public float LookSpeed {  get => lookSpeed; }
    public float Distance { get => distance; }

    void Start()
    {
        nowHP = maxHP;      // �̗͂��ő�ɂ���
        isAtk = false;      // �U��������Ȃ�
        // �N�[���_�E���K�p
        for (int i = 0; i < atkDatas.Length; ++i) atkDatas[i].nowCD = atkDatas[i].maxCD;
 
    }

    void Update()
    {
        // ���Ŕ�\��
        if (nowHP <= 0f)
        { 
            gameObject.SetActive(false); // HP���O�ɂȂ������\��    
            SceneManager.Instance.ChangeScene("StageSelect");
            Debug.Log("HP���O�ɂȂ����̂Ŕ�\��");
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
    }
        
}
