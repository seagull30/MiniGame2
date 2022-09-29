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

    private string SAVE_DATA_DIRECTORY = "/Save"; //저장할 폴더 경로
    private string SAVE_USER = "/SaveUser.txt"; //유저 파일
    private string SAVE_FILENAME_ACHIEVEMENT = "/SaveFileAchievement.txt"; //파일 이름


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
                changeText.text = score + stageScore + "점";
            }
            if (_childs[i].name == "Score")
            {
                changeText = _childs[i].GetComponent<TextMeshProUGUI>();
                changeText.text = score + "점";
            }
            if (_childs[i].name == "Bonus")
            {
                changeText = _childs[i].GetComponent<TextMeshProUGUI>();
                changeText.text = stageScore + "점";
            }
        }


        //게임 오버 됬을때 json에 데이터 저장
        updateData(score + stageScore);


        //게임 오버 됬을때 업적 달성했는지 체크하고 달성했으면 업적 추가
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
            //전체 읽어 오기
            string loadJson = File.ReadAllText(Application.persistentDataPath + SAVE_DATA_DIRECTORY + SAVE_FILENAME_ACHIEVEMENT);
            _saveDataAchievement = JsonUtility.FromJson<SaveDataAchievement>(loadJson);

            //전체 레코드 불러오기
            for (int i = 0; i < _saveDataAchievement.Name.Count; i++)
            {
                //해당 컬럼이 false일때 실행
                if (_saveDataAchievement.Completion[i] == false)
                {

                    //조건 1일때
                    if (_saveDataAchievement.Conditional_Type[i] == 1)
                    {
                        //점수가 레코드 값보다 넘었으면 업적 달성
                        if (_saveDataAchievement.Conditional[i] > score)
                        {
                            _saveDataAchievement.Completion[i] = true;
                        }
                    }
                    //조건 2일때
                    else if (_saveDataAchievement.Conditional_Type[i] == 2)
                    {
                        //아이디가 레코드보다 높을때
                        if (BackGroundManager.backgroundID >= _saveDataAchievement.Conditional[i])
                        {
                            _saveDataAchievement.Completion[i] = true;
                        }
                    }
                    //조건 3일때
                    else if (_saveDataAchievement.Conditional_Type[i] == 3)
                    {
                        int allcrashTime = saveUser.AllCrashTimes;
                        //충돌 횟수가 100회 이상일때
                        if (allcrashTime >= _saveDataAchievement.Conditional[i])
                        {
                            _saveDataAchievement.Completion[i] = true;
                        }
                    }
                    //조건 4일때
                    else if (_saveDataAchievement.Conditional_Type[i] == 4)
                    {
                        int allplayTime = saveUser.AllPlayTimeS;
                        //플레이 시간이 초단위 이상일때
                        if (allplayTime >= _saveDataAchievement.Conditional[i])
                        {
                            _saveDataAchievement.Completion[i] = true;
                        }
                    }

                }
            }

            string json = JsonUtility.ToJson(_saveDataAchievement);//제이슨화
            File.WriteAllText(Application.persistentDataPath + SAVE_DATA_DIRECTORY + SAVE_FILENAME_ACHIEVEMENT, json);

        }


    }



    public void updateData(int score)
    {
        if (File.Exists(Application.persistentDataPath + SAVE_DATA_DIRECTORY + SAVE_USER))
        {
            //전체 읽어 오기
            string loadJson = File.ReadAllText(Application.persistentDataPath + SAVE_DATA_DIRECTORY + SAVE_USER);
            saveUser = JsonUtility.FromJson<SaveUser>(loadJson);

            //새로운 레코드 추가후 다시 저장시키기
            //스코어 추가
            saveUser.UserScore.Add(score);

            //시간 업데이트
            int allplayTime = saveUser.AllPlayTimeS;
            saveUser.AllPlayTimeS = allplayTime + (int)playTimer;

            //부딪친 횟수 업데이트
            int allcrashTime = saveUser.AllCrashTimes;
            saveUser.AllCrashTimes = allcrashTime + 1;

            string jsonUser = JsonUtility.ToJson(saveUser);//제이슨화
            File.WriteAllText(Application.persistentDataPath + SAVE_DATA_DIRECTORY + SAVE_USER, jsonUser);


            Debug.Log("로드완료");
        }
        else
        {
            Debug.Log("세이브 파일이 없습니다.");
        }
    }




}
