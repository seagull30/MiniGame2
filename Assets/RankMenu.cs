using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class RankMenu : MonoBehaviour
{

    public SaveUser saveUser = new SaveUser();

    [SerializeField]
    private GameObject poolingObjectPrefab;


    private string SAVE_DATA_DIRECTORY; //저장할 폴더 경로
    private string SAVE_USER = "/SaveUser.txt"; //유저 파일

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
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_USER);
            saveUser = JsonUtility.FromJson<SaveUser>(loadJson);

            var newObjtemp = Instantiate(poolingObjectPrefab, transform).GetComponent<TextMeshProUGUI>();
            newObjtemp.gameObject.SetActive(true);
            newObjtemp.text = " ";

            //유저 스코어 리스트 + 날짜 불러오기
            for (int i = 0; i < saveUser.UserScore.Count; i++)
            {
                var newObj = Instantiate(poolingObjectPrefab, transform).GetComponent<TextMeshProUGUI>();
                newObj.gameObject.SetActive(true);
                newObj.text = saveUser.UserScore[i].ToString() + " 점 " + saveUser.UserScoreDate[i];
                var newObj2 = Instantiate(poolingObjectPrefab, transform).GetComponent<TextMeshProUGUI>();
                newObj2.gameObject.SetActive(true);
                newObj2.text = " ";
            }

        }
    }
}
