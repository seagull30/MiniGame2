using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlatform : MonoBehaviour
{
    float moveSpeed = 1f;
    private void Awake()
    {
        Invoke("selfDeactivate", 3f);
    }

    private void Update()
    {
        float _moveDistance = moveSpeed * Time.deltaTime;
        transform.position+=new Vector3(0, -_moveDistance, 0);
    }

    private void selfDeactivate()
    {
        gameObject.SetActive(false);
    }
}
