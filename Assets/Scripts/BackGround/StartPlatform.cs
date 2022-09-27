using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlatform : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;


    private void Awake()
    {
        Invoke("selfDeactivate", 3f);
       // StartCoroutine(fallingPlatform());
    }
    private void OnEnable()
    {
        GameManager.Instance.playerOnCiling += OnUPMove;
    }

    //IEnumerator fallingPlatform()
    //{
    //    yield return new WaitForSeconds(1f);
    //    while(true)
    //    {
    //        float _moveDistance = moveSpeed * Time.deltaTime;
    //        transform.position += new Vector3(0, -_moveDistance, 0);
    //        yield return null;
    //    }
    //}

    public void OnUPMove(float distence)
    {
        transform.position += new Vector3(0, -distence, 0);
    }


    private void selfDeactivate()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        GameManager.Instance.playerOnCiling -= OnUPMove;
    }
}
