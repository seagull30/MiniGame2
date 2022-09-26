using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    private Transform[] _backgrounds;
    private int _childsCount;

    private float _upPosY = 0f;
    private float _downPosY = 0f;

    private float _xScreenHalfSize;
    private float _yScreenHalfSize;

    private bool prevBackgroundIndex;

    private readonly string BACKGROUND_SPRITES_FILENAME = "Assets/Resources/BackgroundSprites/";
    private readonly string SCRIPTSOBJECTS_BACKGROUND_FILENAME = "Assets/Resources/ScriptableData/Background/"; //���� �̸�
    public static BackGroundItem[] backgroundData;

    private void Awake()
    {
        _childsCount = transform.childCount;
        _backgrounds = new Transform[_childsCount];

        for (int i = 0; i < _childsCount; ++i)
        {
            _backgrounds[i] = transform.GetChild(i);
        }
        prevBackgroundIndex = false;
    }

    private void OnEnable()
    {
        GameManager.Instance.playerOnCiling += OnUPMove;
        backgroundData = Resources.LoadAll<BackGroundItem>(SCRIPTSOBJECTS_BACKGROUND_FILENAME);
        for(int i = 0; i < backgroundData.Length; ++i)
        {
            Debug.Log(backgroundData[i].id);

        }        
    }

    void Start()
    {
        _yScreenHalfSize = Camera.main.orthographicSize;
        _xScreenHalfSize = _yScreenHalfSize * Camera.main.aspect;

        _upPosY = _yScreenHalfSize * 2 * _backgrounds.Length;
        _downPosY = -_yScreenHalfSize * 2;
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
        if (GameManager.Instance.Score > 1000)
        {
            Sprite changeSprite = _backgrounds[index].GetComponent<SpriteRenderer>().sprite;

        }

    }

    private void OnDisable()
    {
        GameManager.Instance.playerOnCiling -= OnUPMove;

    }
}
