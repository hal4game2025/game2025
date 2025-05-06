using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    // �U�����\����
    [System.Serializable] public struct AtkData
    {
        [CustomLabel("�N�[���_�E��(�b)")] public float maxCD;
        [CustomLabel("�U����")] public float atkDamage;
        [CustomLabel("�U���\����")] public float atkRange;
        [HideInInspector] public float nowCD;
    }

    [SerializeField, CustomLabel("�ő�̗�")]   float maxHP = 100f;
    [SerializeField, CustomLabel("���݂̗̑�")] float nowHP = 100f;
    [SerializeField, CustomLabel("�ړ��X�s�[�h")] float moveSpeed = 10f;
    [SerializeField, CustomLabel("�U��������x")] float lookSpeed = 1f;
    [SerializeField, CustomLabel("�ő�ڋߋ���")] float distance  = 20f;
    [SerializeField, CustomLabel("�U�����")] public AtkData[] atkData;
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
        for (int i = 0; i < atkData.Length; ++i) atkData[i].nowCD = atkData[i].maxCD;
    }

    void Update()
    {
        // ���Ŕ�\��
        if (nowHP <= 0f) gameObject.SetActive(false);
    }

    /// <summary>
    /// �_���[�W����
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(float damage) 
    {
        if (nowHP <= 0f) return;
        nowHP -= damage;
    }
        
}
