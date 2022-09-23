using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlatform : MonoBehaviour
{
    float moveSpeed = 1f;
    private void Awake()
    {
        Invoke("selfDeactivate", 3f);
        StartCoroutine(fallingPlatform());
    }

    IEnumerator fallingPlatform()
    {
        yield return new WaitForSeconds(1f);
        while(true)
        {
            float _moveDistance = moveSpeed * Time.deltaTime;
            transform.position += new Vector3(0, -_moveDistance, 0);
            yield return null;
        }
    }

    private void selfDeactivate()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
    }
}
