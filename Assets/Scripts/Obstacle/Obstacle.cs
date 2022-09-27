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
/// ��ֹ� ���� �� ��ġ ���
/// </summary>
public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private ObstacleRange[] obItems;  //  ��ֹ� �迭

    [SerializeField]
    private GameObject[] objectSpawners; //������ �迭

    Dictionary<int, ObstacleRange> obstacles = new Dictionary<int, ObstacleRange>();  

    public SaveDataObject saveObstacle = new SaveDataObject();

    private string SAVE_DATA_DIRECTORY; //������ ���� ���
    private string SAVE_FILENAME_OBJECT = "/SaveFileObject.txt"; //��ֹ� ����

    //���� �������� ������ �޾ƿ��� ������ ������� ���� �迭 �ε����� �����ϱ�    

    void Start()
    {

        SAVE_DATA_DIRECTORY = Application.dataPath + "/Save/";

        if (!Directory.Exists(SAVE_DATA_DIRECTORY)) //�ش� ��ΰ� �������� �ʴ´ٸ�
        {
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);//���� ����(��� ����)
        }

        LoadDataUser();
    }

    public void LoadDataUser()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME_OBJECT))
        {
            //��ü �о� ����
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME_OBJECT);
            saveObstacle = JsonUtility.FromJson<SaveDataObject>(loadJson);

            //���� ���ھ� ����Ʈ + ��¥ �ҷ�����
            for (int i = 0; i < saveObstacle.Object_Image.Count; i++)
            {
                //��ųʸ��� ���� �����͸� ��´�.
                obstacles.Add( i , new ObstacleRange(i, saveObstacle.Spawn_Min_Score[i], saveObstacle.Spawn_Max_Score[i]));
            }

        }
    }


    //��ųʸ����� ������ ���ϰ� �ش� ���� �����϶� ��ֹ� ������ �Ѵ�.
    private void Update()
    {
        //��ųʸ��� ��� �迭�� �ݺ��� ����
        for(int i = 0; i < obstacles.Count; i++)
        {
            //0�϶��� ��ųʸ��� ���� �ʴ´�.
            if (saveObstacle.Object_Image[i] != "0")
            {
                if (GameManager.Instance.Score > obstacles[i].getMin() && GameManager.Instance.Score < obstacles[i].getMax())
                {
                    //���ھ� ���� ���϶� ������Ʈ Ȱ��ȭ ��Ȱ��ȭ
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
