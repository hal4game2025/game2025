using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField, CustomLabel("�ő�̗�")]   float maxHP = 100f;
    [SerializeField, CustomLabel("���݂̗̑�")] float nowHP = 100f;
    [SerializeField, CustomLabel("�U��������x")] float lookSpeed = 1f;

    [CustomLabel("�d������")] public float stiffnessTime = 0f;

    public float MaxHP { get => maxHP; }
    public float HP { get => nowHP; }
    public float LookSpeed { get => lookSpeed; }

    public bool Deadflg { get; set; }
    void Start()
    {
        nowHP = maxHP;      // �̗͂��ő�ɂ���
    }

    void Update()
    {
        if (nowHP <= 0f)
        { 
            gameObject.SetActive(false); // HP���O�ɂȂ������\��
            Deadflg = true; // ���S�t���O�𗧂Ă�
            //SceneManager.Instance.ChangeScene("ResultScene");
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
