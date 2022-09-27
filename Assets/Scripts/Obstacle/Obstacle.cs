/*using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEditor;


public class ObstacleRange
{
    private int id;
    private int min;
    private int max;

    public ObstacleRange(int id, int min, int max)
    {
        this.id = id;
        this.min = min;
        this.max = max;
    }   

    public int getId()
    {
        return id;
    }

    public int getMin()
    {
        return min;
    }

    public int getMax()
    {
        return max;
    }
}


/// <summary>
/// 장애물 생산 및 배치 담당
/// </summary>
public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private ObstacleRange[] obItems;  //  장애물 배열

    [SerializeField]
    private GameObject[] objectSpawners; //스포너 배열

    Dictionary<int, ObstacleRange> obstacles = new Dictionary<int, ObstacleRange>();  

    public SaveDataObject saveObstacle = new SaveDataObject();

    private string SAVE_DATA_DIRECTORY; //저장할 폴더 경로
    private string SAVE_FILENAME_OBJECT = "/SaveFileObject.txt"; //장애물 파일

    //게임 시작전에 데이터 받아오고 데이터 기반으로 다음 배열 인덱스로 가게하기    

    void Start()
    {

        SAVE_DATA_DIRECTORY = Application.dataPath + "/Save/";

        if (!Directory.Exists(SAVE_DATA_DIRECTORY)) //해당 경로가 존재하지 않는다면
        {
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);//폴더 생성(경로 생성)
        }

        LoadDataUser();
    }

    public void LoadDataUser()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME_OBJECT))
        {
            //전체 읽어 오기
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME_OBJECT);
            saveObstacle = JsonUtility.FromJson<SaveDataObject>(loadJson);

            //유저 스코어 리스트 + 날짜 불러오기
            for (int i = 0; i < saveObstacle.Object_Image.Count; i++)
            {
                //딕셔너리에 범위 데이터를 담는다.
                obstacles.Add( i , new ObstacleRange(i, saveObstacle.Spawn_Min_Score[i], saveObstacle.Spawn_Max_Score[i]));
            }

        }
    }


    //딕셔너리에서 꺼내서 비교하고 해당 점수 범위일때 장애물 나오게 한다.
    private void Update()
    {
        //딕셔너리에 담긴 배열을 반복문 돌림
        for(int i = 0; i < obstacles.Count; i++)
        {
            //0일때는 딕셔너리에 넣지 않는다.
            if (saveObstacle.Object_Image[i] != "0")
            {
                if (GameManager.Instance.Score > obstacles[i].getMin() && GameManager.Instance.Score < obstacles[i].getMax())
                {
                    //스코어 범위 안일때 오브젝트 활성화 비활성화
                    objectSpawners[i].SetActive(true);
                }
                else
                {
                    objectSpawners[i].SetActive(false);
                }

            }
        }
    }


}*/
