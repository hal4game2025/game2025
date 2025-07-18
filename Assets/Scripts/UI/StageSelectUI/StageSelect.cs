using UnityEngine;
using Cysharp.Threading.Tasks;

public class StageSelect : SingletonMonoBehaviour<StageSelect>
{
    public const int MAX_STAGE_COUNT = 4;  // �ő�X�e�[�W��

    [SerializeField]float deley = 0.5f;  // �X�e�[�W�A�C�R���̕\�����ԍ�

    [SerializeField]
    GameObject[] stageIcons = new GameObject[MAX_STAGE_COUNT];  // �X�e�[�W�I���{�^���̔z��

    [SerializeField]
    GameObject linePrefab;
    [SerializeField] Transform UICanvas;


#if UNITY_EDITOR
    Line[] lines = new Line[3];  // �G�f�B�^�[�ł̃f�o�b�O�p�Ƀ��C�����i�[����z��
#endif

    public GameObject[] StageIcons
    {
        get { return stageIcons; }
        private set { stageIcons = value; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(stageIcons.Length != MAX_STAGE_COUNT)
        {
            Debug.LogError("�X�e�[�W��������");
        }

        ShowStageIcons();  // ���ԍ��ŃX�e�[�W�A�C�R����\�����郁�\�b�h���Ăяo��
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.T))
        {
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] != null)
                {
                    Destroy(lines[i].gameObject);  // �G�f�B�^�[�ł̃f�o�b�O�p�Ƀ��C�����폜
                }
            }

            ShowStageIcons();  // Enter�L�[�������ꂽ��X�e�[�W�A�C�R�����ĕ\��
        }
#endif
    }

    // ���ԍ��ŃX�e�[�W�A�C�R����\�����郁�\�b�h
    async void ShowStageIcons()
    {
        for (int i = 0; i < stageIcons.Length; i++)
        {
            stageIcons[i].SetActive(false);  // �X�e�[�W�A�C�R�����\���ɂ���
        }


        for (int i = 0; i < stageIcons.Length; i++)
        {
            if(i != 0)
            {
                await UniTask.Delay((int)(deley * 1000));
            }

            stageIcons[i].SetActive(true);  // �X�e�[�W�A�C�R����\��

            if (i < stageIcons.Length - 1)
            {
                CreateLine(i);
            }
        }
    }


    async void CreateLine(int index)
    {

        await UniTask.Delay((int)(deley * 1000));


        Line line = Instantiate(linePrefab, UICanvas).GetComponent<Line>();
#if UNITY_EDITOR
        lines[index] = line;  // �G�f�B�^�[�ł̃f�o�b�O�p�Ƀ��C�����i�[
#endif

        await UniTask.Yield(); // 1�t���[���҂�
        line.Active(stageIcons[index].transform.position, stageIcons[index + 1].transform.position);
    }
}
