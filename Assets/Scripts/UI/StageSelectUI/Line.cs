using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] GameObject linePrefab;

    private void Start()
    {
        transform.SetSiblingIndex(1);
        gameObject.SetActive(false);  // 初期状態では非表示にする
    }

    public void Active(in Vector3 start,in Vector3 end)
    {
        // 角度を計算して回転を適用
        transform.position = (start + end) / 2;  // 中間点に配置
        Vector3 vec = end - start;
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;


        transform.rotation = Quaternion.Euler(0, 0, angle);

        gameObject.SetActive(true);  // アクティブにする
    }

    void OnBecameVisible()
    {
        // アニメーションを開始する
        GetComponent<Animator>().Play("move", 0, 0.0f);
    }
}
