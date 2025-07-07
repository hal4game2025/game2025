using UnityEngine;

/// <summary>
/// 行動の情報をまとめて管理する
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/ActionData")]
public class ActionData : ScriptableObject
{
    //[SerializeField, Tooltip("攻撃力")] float atkPower = 0f;
    [SerializeField, Tooltip("最大判断範囲")] float maxRange  = 0f;
    [SerializeField, Tooltip("最小判断範囲")] float minRange  = 0f;
    [SerializeField, Tooltip("最大クールダウン")] float maxCD = 0f;
    [SerializeField, Tooltip("硬直時間")] float stiffnessTime = 0f;
    [SerializeField, Tooltip("行動アニメーションの番号")] int animNum = 0;

    [HideInInspector] public float currentCD = 0f;  // 実際に更新される変数

    public float MaxRange { get => maxRange; }
    public float MinRange { get => minRange; }
    public float MaxCD { get => maxCD; }
    public float StiffnessTime { get => stiffnessTime; }
    public int AnimNum { get => animNum; }

    /// <summary>
    /// CDを設定
    /// </summary>
    public void SettingCD()
    {
        if (currentCD == maxCD) return;
        currentCD = maxCD;
    }

}
