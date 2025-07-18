using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectBox : MonoBehaviour
{
    Vector3[] positions;
    StageIcon[] stageIcons;  // �X�e�[�W�A�C�R���̔z��

    int index = 0;  // ���݂̑I���C���f�b�N�X

    [SerializeField] Texture2D[] HpTex = new Texture2D[2];
    [SerializeField] RawImage[] HpUI = new RawImage[20];
    private RawImage[] playerHp;

    [SerializeField] int[] playerHP = new int[3]; // �e�X�e�[�W�̃v���C���[HP�̍ő�l

    [SerializeField] Image[] resultImage = new Image[3];
    [SerializeField] Sprite[] senmetuSprite = new Sprite[3];
    [SerializeField] Sprite[] hidanSprite = new Sprite[3];
    [SerializeField] Sprite[] bairituSprite = new Sprite[3];


    private bool flag = false;

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
        if(Input.GetAxisRaw("Horizontal")==0)
            flag=false;

        Move();  // �I���{�b�N�X�̈ړ�������

        if(Input.GetKeyDown(KeyCode.Return))
        {
            stageIcons[index].OnSelected(); // Enter�L�[�������ꂽ��I�����ꂽ�X�e�[�W�A�C�R���̃N���b�N�C�x���g���Ăяo��
        }

        switch (index)
        {
            case 0:
                hpEnabled(index);
                ChangeSprite(index);
                break;
            case 1:
                hpEnabled(index);
                ChangeSprite(index);

                break;
            case 2:
                hpEnabled(index);
                ChangeSprite(index);

                break;
            case 3:
                hpEnabled(index);
                ChangeSprite(index);

                break;

        }

    }

    private void Move()
    {
        // TODO: �R���g���[���[�~�����Ȃ����Ƃ���
        if (Input.GetAxisRaw("Horizontal")<0&&!flag)
        {
            index--;
            flag = true;  // ���Ɉړ������t���O�𗧂Ă�
            
        }
        
        else if (Input.GetAxisRaw("Horizontal") > 0 && !flag)
        {
            index++;
            flag = true;  // ���Ɉړ������t���O�𗧂Ă�

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

    private void hpEnabled(int index)
    {
        for(int i=0; i < playerHP[index]; i++)
        {
            HpUI[i].enabled = true;
            HpUI[i].texture = HpTex[1]; // HP�����镔���͗L����
        }
        for (int i = playerHP[index]; i < 20; i++)
        {
            HpUI[i].enabled = false;
        }
    }

    private void ChangeSprite(int index)
    {
        resultImage[0].sprite = senmetuSprite[index];
        resultImage[1].sprite = hidanSprite[index];
        resultImage[2].sprite = bairituSprite[index];
    }
}
