using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ObjectItem", menuName = "New ObjectItem/ObjectItem")]
public class ObjectItem : ScriptableObject  // 게임 오브젝트에 붙일 필요 X 
{
    public int id;

    public string itemName; // 아이템의 이름
    public Sprite itemImage; // 아이템의 이미지(인벤 토리 안에서 띄울)
    public GameObject itemPrefab;  // 아이템의 프리팹 (아이템 생성시 프리팹으로 찍어냄)

    public int moveSpeed; //스피드
    public int minScore;  // 스코어
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