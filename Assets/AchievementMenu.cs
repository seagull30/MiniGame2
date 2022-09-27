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


    private string SAVE_DATA_DIRECTORY; //저장할 폴더 경로
    private string SAVE_USER = "/SaveUser.txt"; //유저 파일
    private string SAVE_FILENAME_ACHIEVEMENT = "/SaveFileAchievement.txt"; //파일 이름

    // Start is called before the first frame update
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
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_USER))
        {
            //전체 읽어 오기
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME_ACHIEVEMENT);
            _saveDataAchievement = JsonUtility.FromJson<SaveDataAchievement>(loadJson);

            var newObjtemp = Instantiate(poolingObjectPrefab, transform).GetComponent<TextMeshProUGUI>();
            newObjtemp.gameObject.SetActive(true);
            newObjtemp.text = " ";

            //유저 업적 리스트 불러오기
            for (int i = 0; i < _saveDataAchievement.Name.Count; i++)
            {
                if(_saveDataAchievement.Name[i].ToString() != "0" && _saveDataAchievement.Completion[i].ToString() != "FALSE")
                {
                    //오브젝트 풀링으로 오브젝트 불러오기
                    var newObj = Instantiate(poolingObjectPrefab, transform).GetComponent<TextMeshProUGUI>();
                    newObj.gameObject.SetActive(true);
                    newObj.text = _saveDataAchievement.Name[i];
                    var newObj2 = Instantiate(poolingObjectPrefab, transform).GetComponent<TextMeshProUGUI>();
                    newObj2.gameObject.SetActive(true);
                    newObj2.text = " ";
                }

            }

        }
    }
}
