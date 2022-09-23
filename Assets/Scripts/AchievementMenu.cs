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


    private string SAVE_DATA_DIRECTORY; //������ ���� ���
    private string SAVE_USER = "/SaveUser.txt"; //���� ����
    private string SAVE_FILENAME_ACHIEVEMENT = "/SaveFileAchievement.txt"; //���� �̸�

    // Start is called before the first frame update
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
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_USER))
        {
            //��ü �о� ����
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME_ACHIEVEMENT);
            _saveDataAchievement = JsonUtility.FromJson<SaveDataAchievement>(loadJson);

            var newObjtemp = Instantiate(poolingObjectPrefab, transform).GetComponent<TextMeshProUGUI>();
            newObjtemp.gameObject.SetActive(true);
            newObjtemp.text = " ";

            //���� ���� ����Ʈ �ҷ�����
            for (int i = 0; i < _saveDataAchievement.Name.Count; i++)
            {
                if(_saveDataAchievement.Name[i].ToString() != "0" && _saveDataAchievement.Completion[i].ToString() != "FALSE")
                {
                    //������Ʈ Ǯ������ ������Ʈ �ҷ�����
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
