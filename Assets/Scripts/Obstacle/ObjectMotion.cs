using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMotion : MonoBehaviour
{
    // ObjectItem objectItem;

    private bool _moveDir;
    private float _moveSpeed;

    public ObstacleSpawner ParentScript;
    private bool _ready = false;

    public void Init(string itemname, float speed)
    {
        _moveDir = itemname.Contains("Left") ? true : false;
        _moveSpeed = speed;
        _ready = true;
    }

    private void OnEnable()
    {
        GameManager.Instance.playerOnCiling += OnUPMove;
        if (_ready)
            Invoke("returnPool", 10f);
    }

    private void Update()
    {
        if (_moveDir)
        {
            // ���� ������Ʈ�� �������� ���� �ӵ��� ���� �̵��ϴ� ó��
            transform.Translate(-1 * _moveSpeed * Time.deltaTime, 0f, 0f);
        }
        else
        {
            // ���� ������Ʈ�� �������� ���� �ӵ��� ���� �̵��ϴ� ó��
            transform.Translate(_moveSpeed * Time.deltaTime, 0f, 0f);
        }
    }

    public void OnUPMove(float distence)
    {
        transform.position += new Vector3(0, -distence, 0);
    }

    private void returnPool()
    {
        ParentScript.ReturnObject(this);
    }

    private void OnDisable()
    {
        GameManager.Instance.playerOnCiling -= OnUPMove;
    }

}
