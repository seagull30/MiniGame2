using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TitleBackground : MonoBehaviour
{

    public float speed;
    public Transform[] backgrounds;

    private float _upPosY = 0f;
    private float _downPosY = 0f;

    private float _xScreenHalfSize;
    private float _yScreenHalfSize;

    private void Start()
    {
        _yScreenHalfSize = Camera.main.orthographicSize;
        _xScreenHalfSize = _yScreenHalfSize * Camera.main.aspect;

        _downPosY = -_yScreenHalfSize * 2;
        _upPosY = _yScreenHalfSize * 2 * backgrounds.Length;
    }

    private void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].position += new Vector3(0, -speed, 0) * Time.deltaTime;

            if (backgrounds[i].position.y < _downPosY)
            {
                Vector3 nextPos = backgrounds[i].position;
                nextPos = new Vector3(nextPos.x, nextPos.y + _upPosY, nextPos.z);
                backgrounds[i].position = nextPos;
            }
        }
    }


}
