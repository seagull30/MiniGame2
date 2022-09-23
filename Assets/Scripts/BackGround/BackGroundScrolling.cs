using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScrolling : MonoBehaviour
{
    private Transform[] _backgrounds;
    private int _childsCount;

    private float _upPosY = 0f;
    private float _downPosY = 0f;

    private float _xScreenHalfSize;
    private float _yScreenHalfSize;

    private void Awake()
    {
        _childsCount = transform.childCount;
        _backgrounds = new Transform[_childsCount];

        for (int i = 0; i < _childsCount; ++i)
        {
            _backgrounds[i] = transform.GetChild(i);
        }
        GameManager.Instance.playerOnCiling += OnUPMove;
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
            _backgrounds[i].position += new Vector3(-0, -distence, 0) ;

            if (_backgrounds[i].position.y < _downPosY)
            {
                Vector3 nextPos = _backgrounds[i].position;
                nextPos = new Vector3(nextPos.x, nextPos.y + _upPosY, nextPos.z);
                _backgrounds[i].position = nextPos;
            }
        }
    }

}
