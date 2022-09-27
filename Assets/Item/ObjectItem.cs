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
    public int maxScore;//;
    public AudioClip effectSound;
    public bool effectSoundLoop;
    public int minXValue;
    public int maxXValue;
    public int minYValue;
    public int maxYValue;
    public int maxSpawn;
    public int minSpawnTime;
    public int maxSpawnTime;

}