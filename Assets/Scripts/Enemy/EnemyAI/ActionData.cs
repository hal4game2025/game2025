using UnityEngine;

/// <summary>
/// �s���̏����܂Ƃ߂ĊǗ�����
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/ActionData")]
public class ActionData : ScriptableObject
{
    //[SerializeField, Tooltip("�U����")] float atkPower = 0f;
    [SerializeField, Tooltip("�ő唻�f�͈�")] float maxRange  = 0f;
    [SerializeField, Tooltip("�ŏ����f�͈�")] float minRange  = 0f;
    [SerializeField, Tooltip("�ő�N�[���_�E��")] float maxCD = 0f;
    [SerializeField, Tooltip("�d������")] float stiffnessTime = 0f;
    [SerializeField, Tooltip("�s���A�j���[�V�����̔ԍ�")] int animNum = 0;

    [HideInInspector] public float currentCD = 0f;  // ���ۂɍX�V�����ϐ�

    public float MaxRange { get => maxRange; }
    public float MinRange { get => minRange; }
    public float MaxCD { get => maxCD; }
    public float StiffnessTime { get => stiffnessTime; }
    public int AnimNum { get => animNum; }

    /// <summary>
    /// CD��ݒ�
    /// </summary>
    public void SettingCD()
    {
        if (currentCD == maxCD) return;
        currentCD = maxCD;
    }

}
