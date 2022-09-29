using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEditor;

public class CsvObject
{

    public int ID { get; set; }
    public string Name { get; set; }
    public string Object_Image { get; set; }
    public int Move_Speed { get; set; }
    public int Spawn_Min_Score { get; set; }
    public int Spawn_Max_Score { get; set; }
    public int Effect_Sound { get; set; }
    public bool Effect_Sound_Loop { get; set; }
    public int Min_X_Value { get; set; }
    public int Max_X_Value { get; set; }
    public int Min_Y_Value { get; set; }
    public int Max_Y_Value { get; set; }
    public int Max_Spawn { get; set; }
    public int Min_Spawn_Time { get; set; }
    public int Max_Spawn_Time { get; set; }

}

public class CsvBackGround
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Map_Image { get; set; }
    public int Map_Change_Min_Score { get; set; }
    public int Map_Change_Max_Score { get; set; }
    public string BGM { get; set; }
    public string Map_Change_Effect { get; set; }
    public int Map_Change_Effect_Time { get; set; }
    public int Bonus { get; set; }

}

public class CsvAchievement
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Conditional_Type { get; set; }
    public int Conditional { get; set; }
    public bool Completion { get; set; }
}


[SerializeField]
public class SaveDataObject
{
    //������ ����ȭ�� �Ұ���. ����ȭ�� �Ұ����� �ֵ��� �ִ�.
    public List<int> ID = new List<int>();
    public List<string> Name = new List<string>();
    public List<string> Object_Image = new List<string>();
    public List<int> Move_Speed = new List<int>();
    public List<int> Spawn_Min_Score = new List<int>();
    public List<int> Spawn_Max_Score = new List<int>();
    public List<int> Effect_Sound = new List<int>();
    public List<bool> Effect_Sound_Loop = new List<bool>();
    public List<int> Min_X_Value = new List<int>();
    public List<int> Max_X_Value = new List<int>();
    public List<int> Min_Y_Value = new List<int>();
    public List<int> Max_Y_Value = new List<int>();
    public List<int> Max_Spawn = new List<int>();
    public List<int> Min_Spawn_Time = new List<int>();
    public List<int> Max_Spawn_Time = new List<int>();
}

[SerializeField]
public class SaveDataBackGround
{

    //������ ����ȭ�� �Ұ���. ����ȭ�� �Ұ����� �ֵ��� �ִ�.
    public List<int> ID = new List<int>();
    public List<string> Name = new List<string>();
    public List<string> Map_Image = new List<string>();
    public List<int> Map_Change_Min_Score = new List<int>();
    public List<int> Map_Change_Max_Score = new List<int>();
    public List<string> BGM = new List<string>();
    public List<string> Map_Change_Effect = new List<string>();
    public List<int> Map_Change_Effect_Time = new List<int>();
    public List<int> Bonus = new List<int>();
}

[SerializeField]
public class SaveDataAchievement
{

    //������ ����ȭ�� �Ұ���. ����ȭ�� �Ұ����� �ֵ��� �ִ�.
    public List<int> ID = new List<int>();
    public List<string> Name = new List<string>();
    public List<int> Conditional_Type = new List<int>();
    public List<int> Conditional = new List<int>();
    public List<bool> Completion = new List<bool>();
}

[SerializeField]
public class SaveUser
{
    public string UserName;
    public List<int> UserScore = new List<int>();
    public List<string> UserScoreDate = new List<string>();
    public List<int> UserAchievement = new List<int>();
    public int AllPlayTimeS;
    public int AllCrashTimes;
}



public class DataManager : MonoBehaviour
{

    public SaveDataObject saveObject = new SaveDataObject();
    public SaveDataBackGround saveBackGround = new SaveDataBackGround();
    public SaveDataAchievement saveAchievement = new SaveDataAchievement();
    public SaveUser saveUser = new SaveUser();


    private string SAVE_DATA_DIRECTORY = "/Save"; //������ ���� ���
    private string SAVE_FILENAME_OBJECT = "/SaveFileObject.txt"; //���� �̸�
    private string SAVE_FILENAME_BACKGROUND = "/SaveFileBackGround.txt"; //���� �̸�
    private string SAVE_FILENAME_ACHIEVEMENT = "/SaveFileAchievement.txt"; //���� �̸�
    private string SAVE_USER = "/SaveUser.txt";

    private string path = "/ScriptableData";

    public TextAsset tempTextAssetOb;
    public TextAsset tempTextAssetBg;
    public TextAsset tempTextAssetAh;



    void Start()
    {
        Debug.Log("�� ���� ��� : " + Application.persistentDataPath);


        if (!Directory.Exists(Application.persistentDataPath + SAVE_DATA_DIRECTORY))
        {
            //���� �������� ������ ��������
            Directory.CreateDirectory(Application.persistentDataPath + SAVE_DATA_DIRECTORY);
        }


        JsonUpload();


        if (!Directory.Exists(Application.persistentDataPath + path))
        {
            Directory.CreateDirectory(Application.persistentDataPath + path);
        }
        if (!Directory.Exists(Application.persistentDataPath + path + "/Object"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + path + "/Object");
        }
        if (!Directory.Exists(Application.persistentDataPath + path + "/Background"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + path + "/Background");
        }
        if (!Directory.Exists(Application.persistentDataPath + path + "/Achievement"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + path + "/Achievement");
        }


        //json ������ �����Ҷ� �ȸ���
        if (!File.Exists(Application.persistentDataPath + SAVE_DATA_DIRECTORY + SAVE_FILENAME_OBJECT))
        {
            DataUpload(tempTextAssetOb, 1);
        }
        if (!File.Exists(Application.persistentDataPath + SAVE_DATA_DIRECTORY + SAVE_FILENAME_BACKGROUND))
        {
            DataUpload(tempTextAssetBg, 2);
        }
        if (!File.Exists(Application.persistentDataPath + SAVE_DATA_DIRECTORY + SAVE_FILENAME_ACHIEVEMENT))
        {
            DataUpload(tempTextAssetAh, 3);
        }



    }

    void JsonUpload()
    {
        if (!Directory.Exists(Application.persistentDataPath + SAVE_DATA_DIRECTORY)) //�ش� ��ΰ� �������� �ʴ´ٸ�
        {
            Directory.CreateDirectory(Application.persistentDataPath + SAVE_DATA_DIRECTORY);//���� ����(��� ����)
        }

        FileInfo fileInfo = new FileInfo(Application.persistentDataPath + SAVE_DATA_DIRECTORY + SAVE_USER);

        if (!fileInfo.Exists)
        {

            //���� ���� ����
            saveUser.UserName = "User";
            saveUser.AllCrashTimes = 0;
            saveUser.AllPlayTimeS = 0;
            /* //�ӽ� ������ ����
             saveUser.UserScore.Add(1000);
             saveUser.UserScore.Add(2000);
             saveUser.UserScore.Add(3000);
             saveUser.UserScore.Add(4000);

             saveUser.UserScoreDate.Add("asdasd");
             saveUser.UserScoreDate.Add("asdasd");
             saveUser.UserScoreDate.Add("asdasd");
             saveUser.UserScoreDate.Add("asdasd");
             */

            string jsonUser = JsonUtility.ToJson(saveUser);//���̽�ȭ
            File.WriteAllText(Application.persistentDataPath + SAVE_DATA_DIRECTORY + SAVE_USER, jsonUser);
        }

    }


    void DataUpload(TextAsset url, int classIndex)
    {

        Debug.Log("������ �ε�� ����");

        //1. Resources ������ �մ� CSV ������ TextAsset���� �ε���
        //TextAsset : �ؽ�Ʈ ����



        //2. CSV ���� ����
        //Delimiter ������
        //Environment.NewLine ����ó��
        /*CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            NewLine = Environment.NewLine
        };*/

        //3. CSV �Ľ�(���������� �ɼ� ����)
        /*        using (StringReader csvString = new StringReader(url.text))
                {
                    using (CsvReader csv = new CsvReader(csvString, config))
                    {*/


        if (classIndex == 1)
        {
            //�Ľ��� �����͸� class �� �ʵ�� ���������
            //�ڸ�ƾ�� ������ ����
            //IEnumerable<CsvObject> records = csv.GetRecords<CsvObject>();
            saveObject = JsonUtility.FromJson<SaveDataObject>(url.ToString());

            string json = JsonUtility.ToJson(saveObject);//���̽�ȭ
            File.WriteAllText(Application.persistentDataPath + SAVE_DATA_DIRECTORY + SAVE_FILENAME_OBJECT, json);

        }
        else if (classIndex == 2)
        {
            //�Ľ��� �����͸� class �� �ʵ�� ���������
            //�ڸ�ƾ�� ������ ����
            saveBackGround = JsonUtility.FromJson<SaveDataBackGround>(url.ToString());

            string json = JsonUtility.ToJson(saveBackGround);//���̽�ȭ
            File.WriteAllText(Application.persistentDataPath + SAVE_DATA_DIRECTORY + SAVE_FILENAME_BACKGROUND, json);

        }
        else
        {

            saveAchievement = JsonUtility.FromJson<SaveDataAchievement>(url.ToString());

            string json = JsonUtility.ToJson(saveAchievement);//���̽�ȭ
            File.WriteAllText(Application.persistentDataPath + SAVE_DATA_DIRECTORY + SAVE_FILENAME_ACHIEVEMENT, json);
        }
        /*            }
                }*/

    }

}
