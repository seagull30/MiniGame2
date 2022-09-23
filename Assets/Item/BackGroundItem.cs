using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BackGroundItem", menuName = "New BackGroundItem/BackGroundItem")]
public class BackGroundItem : ScriptableObject  // ���� ������Ʈ�� ���� �ʿ� X 
{
    public int id;

    public string itemName; // �������� �̸�
    public Sprite itemImage; // �������� �̹���(�κ� �丮 �ȿ��� ���)
    public GameObject itemPrefab;  // �������� ������ (������ ������ ���������� ��)

    public int minScore;  // ���ھ�
    public int maxScore;
    public AudioClip BGM;
    public int mapChangeEffect;
    public int mapChangeEffectTime;
    public int bonus;
}