using UnityEngine;

/// <summary>
/// �^�[�Q�b�g�����߂�
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/AI/BehaviorTree/SeeTarget")]
public class BTSeeTarget : BTNode
{
    protected override NodeState NodeUpdate()
    {
        Transform transform = data.status.transform;

        //--- �^�[�Q�b�g�̕�������
        Vector3 pos = target.position - transform.position; // �^�[�Q�b�g����G�l�~�[�܂ł̃x�N�g��
        pos.y = 0f;
        Quaternion look = Quaternion.LookRotation(pos);     // ��]�ɕϊ�
        // �K�p
        data.status.transform.rotation = 
            Quaternion.Slerp(transform.rotation, look, data.status.LookSpeed);

        return NodeState.Success;
    }
}
