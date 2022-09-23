using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AchieveItem", menuName = "New AchieveItem/AchieveItem")]
public class AchieveItem : ScriptableObject  // ���� ������Ʈ�� ���� �ʿ� X 
{
    public int id;

    public string itemName; // �������� �̸�
    public GameObject itemPrefab;  // �������� ������ (������ ������ ���������� ��)

    public int conditinalType; 
    public int conditinal;
    public bool completion;
}