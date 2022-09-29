using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class AchievementMenu : MonoBehaviour
{

    public SaveDataAchievement _saveDataAchievement = new SaveDataAchievement();

    [SerializeField]
    private GameObject poolingObjectPrefab;


    private string SAVE_DATA_DIRECTORY = "/Save"; //저장할 폴더 경로
    private string SAVE_FILENAME_ACHIEVEMENT = "/SaveFileAchievement.txt"; //파일 이름

    // Start is called before the first frame update
    void Start()
    {
        LoadDataUser();
    }

    public void LoadDataUser()
    {


        if (File.Exists(Application.persistentDataPath + SAVE_DATA_DIRECTORY + SAVE_FILENAME_ACHIEVEMENT))
        {
            //전체 읽어 오기
            string loadJson = File.ReadAllText(Application.persistentDataPath + SAVE_DATA_DIRECTORY + SAVE_FILENAME_ACHIEVEMENT);
            _saveDataAchievement = JsonUtility.FromJson<SaveDataAchievement>(loadJson);

            for (int i = 0; i < _saveDataAchievement.Name.Count; i++)
            {
                //오브젝트 생성후 false인지 아닌지에 따라 활성화 비활성화 시키기

                var newObj = Instantiate(poolingObjectPrefab, transform);

                if (_saveDataAchievement.Completion[i] != false)
                {
                    newObj.gameObject.SetActive(true);
                }
                else
                {
                    newObj.gameObject.SetActive(false);
                }
            }
        }
    }
}
