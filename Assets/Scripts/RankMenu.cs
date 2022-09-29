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


    private string SAVE_DATA_DIRECTORY = "/Save"; //������ ���� ���
    private string SAVE_USER = "/SaveUser.txt"; //���� ����

    // Start is called before the first frame update
    void Start()
    {
        LoadDataUser();
    }

    public void LoadDataUser()
    {
        if (File.Exists(Application.persistentDataPath + SAVE_DATA_DIRECTORY + SAVE_USER))
        {
            //��ü �о� ����
            string loadJson = File.ReadAllText(Application.persistentDataPath + SAVE_DATA_DIRECTORY + SAVE_USER);
            saveUser = JsonUtility.FromJson<SaveUser>(loadJson);

            //���� 10���� ���� �ҷ�����
            List<int> scorelist = new List<int>();

            for (int i = 0; i < saveUser.UserScore.Count; i++)
            {
                scorelist.Add(saveUser.UserScore[i] * (-1));
            }

            scorelist.Sort();

            //���� 10���� ���̰� �ϱ�
            for (int i = 0; i < scorelist.Count; i++)
            {
                if (i >= 10)
                {
                    break;
                }

                var newObj = Instantiate(poolingObjectPrefab, transform).GetComponent<TextMeshProUGUI>();
                newObj.gameObject.SetActive(true);
                int num = (-1) * scorelist[i];
                newObj.text = $"{num} �� ";
            }

        }
    }
}
