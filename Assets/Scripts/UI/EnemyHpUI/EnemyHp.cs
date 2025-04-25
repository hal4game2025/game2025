using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyHp : MonoBehaviour
{
    UIDocument enemyHpUIDocument;
    UnityEngine.UIElements.ProgressBar progressBar;
    void Start()
    {
        enemyHpUIDocument = GetComponent<UIDocument>();
        progressBar = enemyHpUIDocument.rootVisualElement.Q<ProgressBar>("EnemyHp");
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            progressBar.value += 10;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            progressBar.value -= 10;
        }
    }
}
