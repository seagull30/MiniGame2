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


    private string SAVE_DATA_DIRECTORY = "/Save"; //저장할 폴더 경로
    private string SAVE_USER = "/SaveUser.txt"; //유저 파일

    // Start is called before the first frame update
    void Start()
    {
        LoadDataUser();
    }

    public void LoadDataUser()
    {
        if (File.Exists(Application.persistentDataPath + SAVE_DATA_DIRECTORY + SAVE_USER))
        {
            //전체 읽어 오기
            string loadJson = File.ReadAllText(Application.persistentDataPath + SAVE_DATA_DIRECTORY + SAVE_USER);
            saveUser = JsonUtility.FromJson<SaveUser>(loadJson);

            //상위 10위권 점수 불러오기
            List<int> scorelist = new List<int>();

            for (int i = 0; i < saveUser.UserScore.Count; i++)
            {
                scorelist.Add(saveUser.UserScore[i] * (-1));
            }

            scorelist.Sort();

            //순위 10위권 보이게 하기
            for (int i = 0; i < scorelist.Count; i++)
            {
                if (i >= 10)
                {
                    break;
                }

                var newObj = Instantiate(poolingObjectPrefab, transform).GetComponent<TextMeshProUGUI>();
                newObj.gameObject.SetActive(true);
                int num = (-1) * scorelist[i];
                newObj.text = $"{num} 점 ";
            }

        }
    }
}
