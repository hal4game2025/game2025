using UnityEngine;
using UnityEngine.AI;

// TODO
/*
    ����
    �U������
    �ǂ�������
 */

public class EnemyState : MonoBehaviour
{
    [SerializeField, CustomLabel("�v���C���[�̍��W")] 
    Transform playerPos;
    [SerializeField, CustomLabel("�ړ��X�s�[�h")] 
    float speed = 10.0f;
    [SerializeField, CustomLabel("�ǂ��܂ŋ߂Â���")] 
    float distance = 20.0f;
    [SerializeField, CustomLabel("�U��������x")] 
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
        // �v���C���[�ւ̕����x�N�g��
        Vector3 pos = playerPos.position - transform.position;
        // ��������]�ɕϊ�
        Quaternion rot = Quaternion.LookRotation(new Vector3(pos.x, 0f, pos.z));
        // �K�p
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, lookSpeed * Time.deltaTime);

        // �v���C���[�Ƃ̋�����distance�����ɂȂ�����~�܂�
        if (Vector3.Distance(transform.position, playerPos.position) < distance)
        {
            return;
        }

        // �v���C���[�Ɍ������Ĉړ�
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(playerPos.position.x, transform.position.y, playerPos.position.z),
            speed * Time.deltaTime);
    }
}
