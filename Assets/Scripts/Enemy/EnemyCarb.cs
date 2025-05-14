using UnityEngine;

public class EnemyCarb : EnemyStateBase
{
    enum ActionKind
    {
        Attack1,    // �U�艺�낷�U��
        Attack2,    // �ガ����
        Max
    }

    [SerializeField, CustomLabel("�U���R���C�_�[")] Collider[] atkCollider;

    ActionKind action;
    
    protected override void EnemyStart()
    {

    }

    protected override void EnemyUpdate()
    {
        for (int i = 0; i < status.atkDatas.Length; ++i)
        {
            // min���������� or max�����傫��������X���[
            if (distance < status.atkDatas[i].minRange || status.atkDatas[i].maxRange < distance) continue;

            // CD�m�F
            if (status.atkDatas[i].nowCD > 0f) continue;

            // �s������
            action = (ActionKind)i;
            // �l�X�V
            status.atkDatas[i].nowCD = status.atkDatas[i].maxCD;    // �N�[���_�E���K�p
            delay = status.atkDatas[i].delay;                       // �f�B���C�K�p
            // �X�e�[�g�ύX
            SetEnemyState((int)EnemyState.Action);
        }
    }

    protected override void Action()
    {
        switch (action)
        {
            case ActionKind.Attack1:
                Attack1();
                break;
            case ActionKind.Attack2:
                Attack2();
                break;
        }
    }

    
    //--- private
    /// <summary>
    /// �U�艺�낷�U��
    /// </summary>
    private void Attack1()
    {
        animator.SetInteger("Action", (int)action);
    }

    /// <summary>
    /// �ガ����
    /// </summary>
    private void Attack2()
    {

    }


    //--- public
    public void ActiveCollider(int atk)
    {
        atkCollider[atk].enabled ^= true;
    }
}
