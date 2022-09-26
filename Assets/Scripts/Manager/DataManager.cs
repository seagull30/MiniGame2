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
    public List<string> Map_Change_Effect = new List<string>();
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

    private readonly string path = "Assets/Resources/ScriptableData/";

    void Start()
    {
        SAVE_DATA_DIRECTORY = Application.dataPath + "/Save/";

        JsonUpload();

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        if (!Directory.Exists(path + "Object/"))
        {
            Directory.CreateDirectory(path + "Object/");
        }
        if (!Directory.Exists(path + "Background/"))
        {
            Directory.CreateDirectory(path + "Background/");
        }
        if (!Directory.Exists(path + "Achievement/"))
        {
            Directory.CreateDirectory(path + "Achievement/");
        }

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

                //유저 정보 저장
                saveUser.UserName = "User";
                //임시 데이터 저장
                saveUser.UserScore.Add(1000);
                saveUser.UserScore.Add(2000);
                saveUser.UserScore.Add(3000);
                saveUser.UserScore.Add(4000);

                saveUser.UserScoreDate.Add("asdasd");
                saveUser.UserScoreDate.Add("asdasd");
                saveUser.UserScoreDate.Add("asdasd");
                saveUser.UserScoreDate.Add("asdasd");


                string jsonUser = JsonUtility.ToJson(saveUser);//제이슨화
                File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_USER, jsonUser);

                if (classIndex == 1)
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

                        ObjectItem scObject = ScriptableObject.CreateInstance<ObjectItem>();
                        scObject.id = record.ID;
                        scObject.itemName = record.Name;
                        
                        //이미지 스프라이트 설정
                       // Sprite objSprite = (Sprite)AssetDatabase.LoadAssetAtPath($"Assets/Images/Object{record.ID}.jpg", typeof(Sprite));
                        //scObject.itemImage = objSprite; //sprite
                        
                        //프리팹 설정
                        //GameObject objPrefab = (GameObject)AssetDatabase.LoadAssetAtPath($"Assets/Prefabs/Object{record.ID}.prefab", typeof(GameObject));
                        //scObject.itemPrefab = objPrefab;
                        
                        scObject.moveSpeed = record.Move_Speed;
                        scObject.minScore = record.Spawn_Min_Score;
                        scObject.maxScore = record.Spawn_Max_Score;
                        scObject.loop = record.Loop;
                        //scObject.effectSound = record.Effect_Sound;
                        scObject.effectSoundLoop = record.Effect_Sound_Loop;
                        scObject.coolTime = record.Cool_Time;

                        AssetDatabase.CreateAsset(scObject, $"Assets/Resources/ScriptableData/Object/Object{record.ID}.asset");
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


                        BackGroundItem scBackGround = ScriptableObject.CreateInstance<BackGroundItem>();
                        scBackGround.id = record.ID;
                        scBackGround.itemName = record.Name;

                        //이미지 스프라이트 설정
                        Sprite objSprite = (Sprite)AssetDatabase.LoadAssetAtPath($"Assets/Images/background{record.Map_Image}.png", typeof(Sprite));
                        scBackGround.itemImage = objSprite; //sprite

                        //프리팹 설정
                        //GameObject objPrefab = (GameObject)AssetDatabase.LoadAssetAtPath($"Assets/Prefabs/Background{record.ID}.prefab", typeof(GameObject));
                        //scBackGround.itemPrefab = objPrefab;

                        scBackGround.minScore = record.Map_Change_Min_Score;
                        scBackGround.maxScore = record.Map_Change_Max_Score;
                        //scBackGround.BGM = record.BGM;

                        //이팩트 스프라이트 설정
                        Sprite objEffect = (Sprite)AssetDatabase.LoadAssetAtPath($"Assets/Images/background{record.Map_Change_Effect}.png", typeof(Sprite));
                        scBackGround.mapChangeEffect = objEffect; //sprite

                        scBackGround.mapChangeEffectTime = record.Map_Change_Effect_Time;
                        scBackGround.bonus = record.Bonus;


                        AssetDatabase.CreateAsset(scBackGround, $"Assets/Resources/ScriptableData/Background/Background{record.ID}.asset");
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

                        AchieveItem scBackGround = ScriptableObject.CreateInstance<AchieveItem>();
                        scBackGround.id = record.ID;
                        scBackGround.itemName = record.Name;

                        //이미지 스프라이트 설정
                        //프리팹 설정
                        //GameObject objPrefab = (GameObject)AssetDatabase.LoadAssetAtPath($"Assets/Prefabs/Achievement{record.ID}.prefab", typeof(GameObject));
                        //scBackGround.itemPrefab = objPrefab;

                        scBackGround.conditinalType = record.Conditional_Type;
                        scBackGround.conditinal = record.Conditional;
                        scBackGround.completion = record.Completion;

                        AssetDatabase.CreateAsset(scBackGround, $"Assets/Resources/ScriptableData/Achievement/Achievement{record.ID}.asset");

                    }

                    string json = JsonUtility.ToJson(saveAchievement);//제이슨화
                    File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME_ACHIEVEMENT, json);
                }
            }
        }

    }






}
