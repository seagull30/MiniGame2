using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class SaveAndLoad : MonoBehaviour
{

    public SaveUser saveUser = new SaveUser();

    [SerializeField]
    private GameObject poolingObjectPrefab;


    private string SAVE_DATA_DIRECTORY; //������ ���� ���
    //private string SAVE_USER = "/SaveUser.txt"; //���� ����

    // Start is called before the first frame update
    void Start()
    {
        /*        SAVE_DATA_DIRECTORY = Application.dataPath + "/Save/";

                if (!Directory.Exists(SAVE_DATA_DIRECTORY)) //�ش� ��ΰ� �������� �ʴ´ٸ�
                {
                    Directory.CreateDirectory(SAVE_DATA_DIRECTORY);//���� ����(��� ����)
                }

                LoadDataUser();*/
    }

    //���� ���� ����
    public void SaveUserData()
    {

    }


    //������Ʈ ����������� ������Ʈ Ǯ���ϱ�
    public void LoadDataObject()
    {

    }

    //��׶��� ���� ������� ������Ʈ Ǯ���ϱ�
    public void LoadDataBackground()
    {

    }

    //���� ���� ������� ������Ʈ Ǯ���ϱ�
    public void LoadDataAchievement()
    {

    }

    //���� ���� ������� ������Ʈ Ǯ���ϱ�
    public void LoadDataUser()
    {

    }


}

