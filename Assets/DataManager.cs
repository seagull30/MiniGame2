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
    //슬롯은 직렬화가 불가능. 직렬화가 불가능한 애들이 있다.
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

    //슬롯은 직렬화가 불가능. 직렬화가 불가능한 애들이 있다.
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

    //슬롯은 직렬화가 불가능. 직렬화가 불가능한 애들이 있다.
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

    private string SAVE_DATA_DIRECTORY; //저장할 폴더 경로
    private string SAVE_FILENAME_OBJECT = "/SaveFileObject.txt"; //파일 이름
    private string SAVE_FILENAME_BACKGROUND = "/SaveFileBackGround.txt"; //파일 이름
    private string SAVE_FILENAME_ACHIEVEMENT = "/SaveFileAchievement.txt"; //파일 이름
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
        if (!Directory.Exists(SAVE_DATA_DIRECTORY)) //해당 경로가 존재하지 않는다면
        {
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);//폴더 생성(경로 생성)
        }
    }


    void DataUpload(string url, int classIndex) 
    {
        //1. Resources 폴더에 잇는 CSV 파일을 TextAsset으로 로드함
        //TextAsset : 텍스트 파일
        TextAsset tempTextAsset = Resources.Load<TextAsset>(url);

        //2. CSV 파일 설정
        //Delimiter 구분자
        //Environment.NewLine 개행처리
        CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ",",
            NewLine = Environment.NewLine
        };

        //3. CSV 파싱(병목지점이 될수 있음)
        using (StringReader csvString = new StringReader(tempTextAsset.text))
        {
            using (CsvReader csv = new CsvReader(csvString, config))
            {

                if(classIndex == 1)
                {
                    //파싱한 데이터를 class 의 필드로 적용시켜줌
                    //코르틴에 넣을수 있음
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

                    string json = JsonUtility.ToJson(saveObject);//제이슨화
                    File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME_OBJECT, json);

                }
                else if(classIndex == 2)
                {
                    //파싱한 데이터를 class 의 필드로 적용시켜줌
                    //코르틴에 넣을수 있음
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
                    string json = JsonUtility.ToJson(saveBackGround);//제이슨화
                    File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME_BACKGROUND, json);

                }
                else
                {
                    //파싱한 데이터를 class 의 필드로 적용시켜줌
                    //코르틴에 넣을수 있음
                    IEnumerable<CsvAchievement> records = csv.GetRecords<CsvAchievement>();

                    foreach (CsvAchievement record in records)
                    {
                        saveAchievement.ID.Add(record.ID);
                        saveAchievement.Name.Add(record.Name);
                        saveAchievement.Conditional_Type.Add(record.Conditional_Type);
                        saveAchievement.Conditional.Add(record.Conditional);
                        saveAchievement.Completion.Add(record.Completion);

                    }

                    string json = JsonUtility.ToJson(saveAchievement);//제이슨화
                    File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME_ACHIEVEMENT, json);
                }
            }
        }

    }






}
