using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ObjectItem", menuName = "New ObjectItem/ObjectItem")]
public class ObjectItem : ScriptableObject  // ���� ������Ʈ�� ���� �ʿ� X 
{
    public int id;

    public string itemName; // �������� �̸�
    public Sprite itemImage; // �������� �̹���(�κ� �丮 �ȿ��� ���)
    public GameObject itemPrefab;  // �������� ������ (������ ������ ���������� ��)

    public int moveSpeed; //���ǵ�
    public int minScore;  // ���ھ�
    public int maxScore;
    public bool loop;
    public AudioClip effectSound;
    public bool effectSoundLoop;
    public int coolTime;
}