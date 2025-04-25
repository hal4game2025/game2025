using UnityEngine;
using UnityEngine.AI;

// TODO
/*
    死ぬ
    攻撃する
    追いかける
 */

public class EnemyState : MonoBehaviour
{
    [SerializeField, CustomLabel("プレイヤーの座標")] 
    Transform playerPos;
    [SerializeField, CustomLabel("移動スピード")] 
    float speed = 10.0f;
    [SerializeField, CustomLabel("どこまで近づくか")] 
    float distance = 20.0f;
    [SerializeField, CustomLabel("振り向き速度")] 
    float lookSpeed = 1f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // プレイヤーへの方向ベクトル
        Vector3 pos = playerPos.position - transform.position;
        // 方向を回転に変換
        Quaternion rot = Quaternion.LookRotation(new Vector3(pos.x, 0f, pos.z));
        // 適用
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, lookSpeed * Time.deltaTime);

        // プレイヤーとの距離がdistance未満になったら止まる
        if (Vector3.Distance(transform.position, playerPos.position) < distance)
        {
            return;
        }

        // プレイヤーに向かって移動
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(playerPos.position.x, transform.position.y, playerPos.position.z),
            speed * Time.deltaTime);
    }
}
