using UnityEngine;

public class StageIcon : MonoBehaviour
{
    Animator animator;  // �A�j���[�^�[�R���|�[�l���g

    void Start()
    {
        animator = GetComponent<Animator>();  // �A�j���[�^�[�R���|�[�l���g���擾
    }

    void OnBecameVisible()
    {
        animator.Play("move", 0, 0.0f);
    }

    // �X�e�[�W�A�C�R�����I�����ꂽ�Ƃ��ɌĂяo����郁�\�b�h
    public void OnSelected()
    {

        SceneManager.Instance.ChangeScene(gameObject.name);
    }
}
