using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMotion : MonoBehaviour
{
    [SerializeField]
    private ObjectItem objectItem;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 20f);
    }

    private void Update()
    {
        if(objectItem.itemName.Contains("Left"))
        {
            // 게임 오브젝트를 왼쪽으로 일정 속도로 평행 이동하는 처리
            transform.Translate(-1 * objectItem.moveSpeed * Time.deltaTime, 0f, 0f);
        }
        else
        {
            // 게임 오브젝트를 왼쪽으로 일정 속도로 평행 이동하는 처리
            transform.Translate(objectItem.moveSpeed * Time.deltaTime, 0f, 0f);
        }

    }
}
