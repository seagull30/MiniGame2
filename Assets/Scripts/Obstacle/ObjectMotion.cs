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
            // ���� ������Ʈ�� �������� ���� �ӵ��� ���� �̵��ϴ� ó��
            transform.Translate(-1 * objectItem.moveSpeed * Time.deltaTime, 0f, 0f);
        }
        else
        {
            // ���� ������Ʈ�� �������� ���� �ӵ��� ���� �̵��ϴ� ó��
            transform.Translate(objectItem.moveSpeed * Time.deltaTime, 0f, 0f);
        }

    }
}
