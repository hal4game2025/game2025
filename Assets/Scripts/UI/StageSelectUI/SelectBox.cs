using UnityEngine;

public class SelectBox : MonoBehaviour
{
    Vector3[] positions;
    StageIcon[] stageIcons;  // �X�e�[�W�A�C�R���̔z��

    int index = 0;  // ���݂̑I���C���f�b�N�X

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        positions = new Vector3[StageSelect.MAX_STAGE_COUNT];  // �X�e�[�W�����̈ʒu���i�[����z���������
        stageIcons = new StageIcon[StageSelect.MAX_STAGE_COUNT];  // �X�e�[�W�A�C�R���̔z���������

        for (int i = 0; i < StageSelect.Instance.StageIcons.Length; i++)
        {
            positions[i] = StageSelect.Instance.StageIcons[i].transform.position;
            stageIcons[i] = StageSelect.Instance.StageIcons[i].GetComponent<StageIcon>();  // �X�e�[�W�A�C�R���̔z����擾
        }

        transform.SetSiblingIndex(3);  // �I���{�b�N�X���őO�ʂɔz�u
    }

    // Update is called once per frame
    void Update()
    {
        Move();  // �I���{�b�N�X�̈ړ�������

        if(Input.GetKeyDown(KeyCode.Return))
        {
            stageIcons[index].OnSelected(); // Enter�L�[�������ꂽ��I�����ꂽ�X�e�[�W�A�C�R���̃N���b�N�C�x���g���Ăяo��
        }
    }

    private void Move()
    {
        // TODO: �R���g���[���[�~�����Ȃ����Ƃ���
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            index--;
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            index++;
        }

        if (index < 0)
        {
            index = StageSelect.MAX_STAGE_COUNT - 1;  // �Ō�̃X�e�[�W�ɖ߂�
        }
        else if (index >= StageSelect.MAX_STAGE_COUNT)
        {
            index = 0;  // �ŏ��̃X�e�[�W�ɖ߂�
        }

        transform.position = positions[index];  // �I���{�b�N�X�̈ʒu���X�V
    }
}
