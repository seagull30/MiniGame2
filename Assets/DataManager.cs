using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class CsvObject
{

    public int ID { get; set; }
    public string Name { get; set; }
    public string Object_Image {get; set;}
    public int Move_Speed { get; set; }
    public int Spawn_Min_Score { get; set; }
    public int Spawn_Max_Score { get; set; }
    public bool Loop { get; set; }
    public string Effect_Sound { get; set; }
    public bool Effect_Sound_Loop { get; set; }
    public int Cool_Time { get; set; }

}

public class CsvBackGround
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Map_Image { get; set; }
    public int Map_Change_Min_Score { get; set; }
    public int Map_Change_Max_Score { get; set; }
    public string BGM { get; set; }
    public int Map_Change_Effect { get; set; }
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
    public List<bool> Loop = new List<bool>();
    public List<string> Effect_Sound = new List<string>();
    public List<bool> Effect_Sound_Loop = new List<bool>();
    public List<int> Cool_Time = new List<int>();
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
    public List<int> Map_Change_Effect = new List<int>();
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
}



public class DataManager : MonoBehaviour
{
    public SaveDataObject saveObject = new SaveDataObject();
    public SaveDataBackGround saveBackGround = new SaveDataBackGround();
    public SaveDataAchievement saveAchievement = new SaveDataAchievement();
    public SaveUser saveUser = new SaveUser();

    private string SAVE_DATA_DIRECTORY; //������ ���� ���
    private string SAVE_FILENAME_OBJECT = "/SaveFileObject.txt"; //���� �̸�
    private string SAVE_FILENAME_BACKGROUND = "/SaveFileBackGround.txt"; //���� �̸�
    private string SAVE_FILENAME_ACHIEVEMENT = "/SaveFileAchievement.txt"; //���� �̸�
    private string SAVE_USER = "/SaveUser.txt";

    void Start()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Save/";

        JsonUpload();
        
        DataUpload("Csv/CSV_obstacle", 1);
        DataUpload("Csv/CSV_background", 2);
        DataUpload("Csv/CSV_achievement", 3);

        
    }

    void JsonUpload()
    {
        if (!Directory.Exists(SAVE_DATA_DIRECTORY)) //�ش� ��ΰ� �������� �ʴ´ٸ�
        {
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);//���� ����(��� ����)
        }
    }


    void DataUpload(string url, int classIndex) 
    {
        //1. Resources ������ �մ� CSV ������ TextAsset���� �ε���
        //TextAsset : �ؽ�Ʈ ����
        TextAsset tempTextAsset = Resources.Load<TextAsset>(url);

        //2. CSV ���� ����
        //Delimiter ������
        //Environment.NewLine ����ó��
        CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            NewLine = Environment.NewLine
        };

        //3. CSV �Ľ�(���������� �ɼ� ����)
        using (StringReader csvString = new StringReader(tempTextAsset.text))
        {
            using (CsvReader csv = new CsvReader(csvString, config))
            {

                if(classIndex == 1)
                {
                    //�Ľ��� �����͸� class �� �ʵ�� ���������
                    //�ڸ�ƾ�� ������ ����
                    IEnumerable<CsvObject> records = csv.GetRecords<CsvObject>();

                    foreach (CsvObject record in records)
                    {
                        saveObject.ID.Add(record.ID);
                        saveObject.Name.Add(record.Name);
                        saveObject.Object_Image.Add(record.Object_Image);
                        saveObject.Move_Speed.Add(record.Move_Speed);
                        saveObject.Spawn_Min_Score.Add(record.Spawn_Min_Score);
                        saveObject.Spawn_Max_Score.Add(record.Spawn_Max_Score);
                        saveObject.Loop.Add(record.Loop);
                        saveObject.Effect_Sound.Add(record.Effect_Sound);
                        saveObject.Effect_Sound_Loop.Add(record.Effect_Sound_Loop);
                        saveObject.Cool_Time.Add(record.Cool_Time);


                    }

                    string json = JsonUtility.ToJson(saveObject);//���̽�ȭ
                    File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME_OBJECT, json);

                }
                else if(classIndex == 2)
                {
                    //�Ľ��� �����͸� class �� �ʵ�� ���������
                    //�ڸ�ƾ�� ������ ����
                    IEnumerable<CsvBackGround> records = csv.GetRecords<CsvBackGround>();

                    foreach (CsvBackGround record in records)
                    {
                        saveBackGround.ID.Add(record.ID);
                        saveBackGround.Name.Add(record.Name);
                        saveBackGround.Map_Image.Add(record.Map_Image);
                        saveBackGround.Map_Change_Min_Score.Add(record.Map_Change_Min_Score);
                        saveBackGround.Map_Change_Max_Score.Add(record.Map_Change_Max_Score);
                        saveBackGround.BGM.Add(record.BGM);
                        saveBackGround.Map_Change_Effect.Add(record.Map_Change_Effect);
                        saveBackGround.Map_Change_Effect_Time.Add(record.Map_Change_Effect_Time);
                        saveBackGround.Bonus.Add(record.Bonus);

                    }
                    string json = JsonUtility.ToJson(saveBackGround);//���̽�ȭ
                    File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME_BACKGROUND, json);

                }
                else
                {
                    //�Ľ��� �����͸� class �� �ʵ�� ���������
                    //�ڸ�ƾ�� ������ ����
                    IEnumerable<CsvAchievement> records = csv.GetRecords<CsvAchievement>();

                    foreach (CsvAchievement record in records)
                    {
                        saveAchievement.ID.Add(record.ID);
                        saveAchievement.Name.Add(record.Name);
                        saveAchievement.Conditional_Type.Add(record.Conditional_Type);
                        saveAchievement.Conditional.Add(record.Conditional);
                        saveAchievement.Completion.Add(record.Completion);

                    }

                    string json = JsonUtility.ToJson(saveAchievement);//���̽�ȭ
                    File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME_ACHIEVEMENT, json);
                }
            }
        }

    }






}
