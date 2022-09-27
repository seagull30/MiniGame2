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


    private string SAVE_DATA_DIRECTORY; //������ ���� ���
    private string SAVE_USER = "/SaveUser.txt"; //���� ����

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
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_USER);
            saveUser = JsonUtility.FromJson<SaveUser>(loadJson);

            var newObjtemp = Instantiate(poolingObjectPrefab, transform).GetComponent<TextMeshProUGUI>();
            newObjtemp.gameObject.SetActive(true);
            newObjtemp.text = " ";

            //���� ���ھ� ����Ʈ + ��¥ �ҷ�����
            for (int i = 0; i < saveUser.UserScore.Count; i++)
            {
                var newObj = Instantiate(poolingObjectPrefab, transform).GetComponent<TextMeshProUGUI>();
                newObj.gameObject.SetActive(true);
                newObj.text = saveUser.UserScore[i].ToString() + " �� " + saveUser.UserScoreDate[i];
                var newObj2 = Instantiate(poolingObjectPrefab, transform).GetComponent<TextMeshProUGUI>();
                newObj2.gameObject.SetActive(true);
                newObj2.text = " ";
            }

        }
    }
}
