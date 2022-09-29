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


    private string SAVE_DATA_DIRECTORY = "/Save"; //������ ���� ���
    private string SAVE_FILENAME_ACHIEVEMENT = "/SaveFileAchievement.txt"; //���� �̸�

    // Start is called before the first frame update
    void Start()
    {
        LoadDataUser();
    }

    public void LoadDataUser()
    {


        if (File.Exists(Application.persistentDataPath + SAVE_DATA_DIRECTORY + SAVE_FILENAME_ACHIEVEMENT))
        {
            //��ü �о� ����
            string loadJson = File.ReadAllText(Application.persistentDataPath + SAVE_DATA_DIRECTORY + SAVE_FILENAME_ACHIEVEMENT);
            _saveDataAchievement = JsonUtility.FromJson<SaveDataAchievement>(loadJson);

            for (int i = 0; i < _saveDataAchievement.Name.Count; i++)
            {
                //������Ʈ ������ false���� �ƴ����� ���� Ȱ��ȭ ��Ȱ��ȭ ��Ű��

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
