using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BackGroundManager : MonoBehaviour
{
    private Transform[] _backgrounds;
    private SpriteRenderer[] _sprites;
    private int _childsCount;

    private float _upPosY = 0f;
    private float _downPosY = 0f;

    private float _xScreenHalfSize;
    private float _yScreenHalfSize;

    private bool prevBackgroundIndex;

    private readonly string SCRIPTSOBJECTS_BACKGROUND_FILENAME = "ScriptableData/Background/"; //파일 이름
    public static BackGroundItem[] backgroundData;
    private int backgroundDataIndex;

    public static int backgroundID;



    private void Awake()
    {
        _childsCount = transform.childCount;
        _backgrounds = new Transform[_childsCount];
        _sprites = new SpriteRenderer[_childsCount];
        for (int i = 0; i < _childsCount; ++i)
        {
            _backgrounds[i] = transform.GetChild(i);
            _sprites[i] = _backgrounds[i].GetComponent<SpriteRenderer>();
        }
        prevBackgroundIndex = false;
    }

    private void OnEnable()
    {
        GameManager.Instance.playerOnCiling += OnUPMove;
        backgroundData = Resources.LoadAll<BackGroundItem>(SCRIPTSOBJECTS_BACKGROUND_FILENAME);
        backgroundDataIndex = 0;
    }

    void Start()
    {
        _yScreenHalfSize = Camera.main.orthographicSize;
        _xScreenHalfSize = _yScreenHalfSize * Camera.main.aspect;

        _upPosY = _yScreenHalfSize * 2 * _backgrounds.Length;
        _downPosY = -_yScreenHalfSize * 2;
        //임시적용 김영훈 2022.9.27
        SoundManager.instance.PlayBGM(backgroundData[backgroundDataIndex].BGM);
        //아이디를 업적 달성 조건에 맞는지 확인이 필요
        backgroundID = backgroundData[backgroundDataIndex].id;
    }
    public void OnUPMove(float distence)
    {
        for (int i = 0; i < _backgrounds.Length; i++)
        {
            _backgrounds[i].position += new Vector3(-0, -distence, 0);

            if (_backgrounds[i].position.y < _downPosY)
            {
                Vector3 nextPos = _backgrounds[i].position;
                nextPos = new Vector3(nextPos.x, nextPos.y + _upPosY, nextPos.z);
                _backgrounds[i].position = nextPos;
                checkChangeBackground(prevBackgroundIndex);
                prevBackgroundIndex = !prevBackgroundIndex;

            }
        }
    }

    private void checkChangeBackground(bool prevBackgroundIndex)
    {
        int index = prevBackgroundIndex ? 1 : 0;
        Debug.Log($" 배경 변화 : {backgroundData[backgroundDataIndex].maxScore}, {backgroundData[backgroundDataIndex].minScore},{GameManager.Instance.Score}");

        if (GameManager.Instance.Score > backgroundData[backgroundDataIndex].maxScore)
        {
            _sprites[index].sprite = backgroundData[backgroundDataIndex].mapChangeEffect;
            _sprites[index].size = new Vector2(_xScreenHalfSize * 2, _yScreenHalfSize * 2);

            ++backgroundDataIndex;
            SoundManager.instance.PlayBGM(backgroundData[backgroundDataIndex].BGM);
            backgroundID = backgroundData[backgroundDataIndex].id;
            return;
        }
        if (GameManager.Instance.Score > backgroundData[backgroundDataIndex].minScore)
        {
            _sprites[index].sprite = backgroundData[backgroundDataIndex].itemImage;
            _sprites[index].size = new Vector2(_xScreenHalfSize * 2, _yScreenHalfSize * 2);
            GameManager.Instance.stageScore = backgroundData[backgroundDataIndex].bonus;
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.playerOnCiling -= OnUPMove;
    }
}
