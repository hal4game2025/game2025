using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyHp : MonoBehaviour
{
    [SerializeField]private EnemyStatus[] enemyStatus;
    [SerializeField] private Transform playerTrasnform;
    [SerializeField]private HammerCollision hammerCollision;
    private float time;
    UIDocument enemyHpUIDocument;
    UnityEngine.UIElements.ProgressBar progressBar;


    void Start()
    {
        
        enemyHpUIDocument = GetComponent<UIDocument>();
        progressBar = enemyHpUIDocument.rootVisualElement.Q<ProgressBar>("EnemyHp");
       time = 0;
        
    }

    void Update()
    {
        if (enemyStatus == null || enemyStatus.Length == 0 || playerTrasnform == null) return;

        //ここがnull
        List<EnemyCollider> enemyColliderList = hammerCollision.GetEnemyColliderList(); // HammerCollisionから敵のコライダーリストを更新


       if( enemyColliderList.Count==0 || time >= 2.0f)
        {
            EnemyStatus nearestEnemy = null;
            float minSqrDist = float.MaxValue;

            //敵キャラの中でプレイヤーに最も近い敵を探す
            foreach (var enemy in enemyStatus)
            {
                //敵がnullか、既に死亡している場合はスキップ
                if (enemy == null || enemy.Deadflg) continue;
                float sqrDist = (enemy.transform.position - playerTrasnform.position).sqrMagnitude;
                if (sqrDist < minSqrDist)
                {
                    minSqrDist = sqrDist;
                    nearestEnemy = enemy;
                }
            }

            if (nearestEnemy != null)
            {
                //最も近い敵のHPをUIに反映
                progressBar.value = nearestEnemy.HP;
                time = 0;
            }
       }
       else
       {
            time += Time.deltaTime;
            for (int i = 0; i < enemyColliderList.Count; i++)
            {
                progressBar.value = enemyColliderList[i].GetEnemyStatus().HP;
            }
       }
        
        

    }
}
