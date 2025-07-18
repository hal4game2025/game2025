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

    void Start()
    {
        nowHP = maxHP-1;      // �̗͂��ő�ɂ���
    }                         // HPUI�łǂ����悤���Ȃ��o�O�����邽��-1�����Ă��������Ă܂�

    void Update()
    {
        if (nowHP <= 0f)
        { 
            gameObject.SetActive(false); // HP���O�ɂȂ������\��    
            SceneManager.Instance.ChangeScene("ResultScene");
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
