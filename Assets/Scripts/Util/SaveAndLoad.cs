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


    private string SAVE_DATA_DIRECTORY; //저장할 폴더 경로
    //private string SAVE_USER = "/SaveUser.txt"; //유저 파일

    // Start is called before the first frame update
    void Start()
    {
        /*        SAVE_DATA_DIRECTORY = Application.dataPath + "/Save/";

                if (!Directory.Exists(SAVE_DATA_DIRECTORY)) //해당 경로가 존재하지 않는다면
                {
                    Directory.CreateDirectory(SAVE_DATA_DIRECTORY);//폴더 생성(경로 생성)
                }

                LoadDataUser();*/
    }

    //유저 정보 저장
    public void SaveUserData()
    {

    }


    //오브젝트 정보기반으로 오브젝트 풀링하기
    public void LoadDataObject()
    {

    }

    //백그라운드 정보 기반으로 오브젝트 풀링하기
    public void LoadDataBackground()
    {

    }

    //성과 정보 기반으로 오브젝트 풀링하기
    public void LoadDataAchievement()
    {

    }

    //유저 정보 기반으로 오브젝트 풀링하기
    public void LoadDataUser()
    {

    }


}

