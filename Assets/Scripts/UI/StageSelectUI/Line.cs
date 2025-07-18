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
        gameObject.SetActive(false);  // ������Ԃł͔�\���ɂ���
    }

    public void Active(in Vector3 start,in Vector3 end)
    {
        // �p�x���v�Z���ĉ�]��K�p
        transform.position = (start + end) / 2;  // ���ԓ_�ɔz�u
        Vector3 vec = end - start;
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;


        transform.rotation = Quaternion.Euler(0, 0, angle);

        gameObject.SetActive(true);  // �A�N�e�B�u�ɂ���
    }

    void OnBecameVisible()
    {
        // �A�j���[�V�������J�n����
        GetComponent<Animator>().Play("move", 0, 0.0f);
    }
}
