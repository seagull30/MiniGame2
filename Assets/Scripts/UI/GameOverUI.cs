using System.IO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    private GameObject[] _childs;
    private int _childCount;

    TextMeshProUGUI changeText;

    public SaveUser saveUser = new SaveUser();
    public SaveDataAchievement _saveDataAchievement = new SaveDataAchievement();

    private string SAVE_DATA_DIRECTORY = "/Save"; //������ ���� ���
    private string SAVE_USER = "/SaveUser.txt"; //���� ����
    private string SAVE_FILENAME_ACHIEVEMENT = "/SaveFileAchievement.txt"; //���� �̸�


    private float playTimer = 0f;

    private void Update()
    {
        playTimer += Time.deltaTime;
    }

    private void Awake()
    {
        _childCount = transform.childCount;
        _childs = new GameObject[_childCount];

        for (int i = 0; i < _childCount; ++i)
        {
            _childs[i] = transform.GetChild(i).gameObject;
            _childs[i].SetActive(false);
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.OnGameEnd += Activate;
    }

    public void Activate(int score, int stageScore)
    {
        for (int i = 0; i < _childCount; ++i)
        {
            _childs[i].SetActive(true);
            if (_childs[i].name == "Final Score")
            {
                changeText = _childs[i].GetComponent<TextMeshProUGUI>();
                changeText.text = score + stageScore + "��";
            }
            if (_childs[i].name == "Score")
            {
                changeText = _childs[i].GetComponent<TextMeshProUGUI>();
                changeText.text = score + "��";
            }
            if (_childs[i].name == "Bonus")
            {
                changeText = _childs[i].GetComponent<TextMeshProUGUI>();
                changeText.text = stageScore + "��";
            }
        }


        //���� ���� ������ json�� ������ ����
        updateData(score + stageScore);


        //���� ���� ������ ���� �޼��ߴ��� üũ�ϰ� �޼������� ���� �߰�
        achievementUpdateData(score + stageScore);

    }
    private void OnDisable()
    {
        GameManager.Instance.OnGameEnd -= Activate;
    }

    public void achievementUpdateData(int score)
    {

        if (File.Exists(Application.persistentDataPath + SAVE_DATA_DIRECTORY + SAVE_FILENAME_ACHIEVEMENT))
        {
            //��ü �о� ����
            string loadJson = File.ReadAllText(Application.persistentDataPath + SAVE_DATA_DIRECTORY + SAVE_FILENAME_ACHIEVEMENT);
            _saveDataAchievement = JsonUtility.FromJson<SaveDataAchievement>(loadJson);

            //��ü ���ڵ� �ҷ�����
            for (int i = 0; i < _saveDataAchievement.Name.Count; i++)
            {
                //�ش� �÷��� false�϶� ����
                if (_saveDataAchievement.Completion[i] == false)
                {

                    //���� 1�϶�
                    if (_saveDataAchievement.Conditional_Type[i] == 1)
                    {
                        //������ ���ڵ� ������ �Ѿ����� ���� �޼�
                        if (_saveDataAchievement.Conditional[i] > score)
                        {
                            _saveDataAchievement.Completion[i] = true;
                        }
                    }
                    //���� 2�϶�
                    else if (_saveDataAchievement.Conditional_Type[i] == 2)
                    {
                        //���̵� ���ڵ庸�� ������
                        if (BackGroundManager.backgroundID >= _saveDataAchievement.Conditional[i])
                        {
                            _saveDataAchievement.Completion[i] = true;
                        }
                    }
                    //���� 3�϶�
                    else if (_saveDataAchievement.Conditional_Type[i] == 3)
                    {
                        int allcrashTime = saveUser.AllCrashTimes;
                        //�浹 Ƚ���� 100ȸ �̻��϶�
                        if (allcrashTime >= _saveDataAchievement.Conditional[i])
                        {
                            _saveDataAchievement.Completion[i] = true;
                        }
                    }
                    //���� 4�϶�
                    else if (_saveDataAchievement.Conditional_Type[i] == 4)
                    {
                        int allplayTime = saveUser.AllPlayTimeS;
                        //�÷��� �ð��� �ʴ��� �̻��϶�
                        if (allplayTime >= _saveDataAchievement.Conditional[i])
                        {
                            _saveDataAchievement.Completion[i] = true;
                        }
                    }

                }
            }

            string json = JsonUtility.ToJson(_saveDataAchievement);//���̽�ȭ
            File.WriteAllText(Application.persistentDataPath + SAVE_DATA_DIRECTORY + SAVE_FILENAME_ACHIEVEMENT, json);

        }


    }



    public void updateData(int score)
    {
        if (File.Exists(Application.persistentDataPath + SAVE_DATA_DIRECTORY + SAVE_USER))
        {
            //��ü �о� ����
            string loadJson = File.ReadAllText(Application.persistentDataPath + SAVE_DATA_DIRECTORY + SAVE_USER);
            saveUser = JsonUtility.FromJson<SaveUser>(loadJson);

            //���ο� ���ڵ� �߰��� �ٽ� �����Ű��
            //���ھ� �߰�
            saveUser.UserScore.Add(score);

            //�ð� ������Ʈ
            int allplayTime = saveUser.AllPlayTimeS;
            saveUser.AllPlayTimeS = allplayTime + (int)playTimer;

            //�ε�ģ Ƚ�� ������Ʈ
            int allcrashTime = saveUser.AllCrashTimes;
            saveUser.AllCrashTimes = allcrashTime + 1;

            string jsonUser = JsonUtility.ToJson(saveUser);//���̽�ȭ
            File.WriteAllText(Application.persistentDataPath + SAVE_DATA_DIRECTORY + SAVE_USER, jsonUser);


            Debug.Log("�ε�Ϸ�");
        }
        else
        {
            Debug.Log("���̺� ������ �����ϴ�.");
        }
    }




}
