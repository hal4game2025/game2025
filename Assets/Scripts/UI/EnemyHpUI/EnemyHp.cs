using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyHp : MonoBehaviour
{
    [SerializeField]private EnemyStatus enemyStatus;
    UIDocument enemyHpUIDocument;
    UnityEngine.UIElements.ProgressBar progressBar;
    void Start()
    {
        
        enemyHpUIDocument = GetComponent<UIDocument>();
        progressBar = enemyHpUIDocument.rootVisualElement.Q<ProgressBar>("EnemyHp");
        
    }

    void Update()
    {
        progressBar.value = enemyStatus.HP;

    }
}
