using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BackGroundItem", menuName = "New BackGroundItem/BackGroundItem")]
public class BackGroundItem : ScriptableObject  // 게임 오브젝트에 붙일 필요 X 
{
    public int id;

    public string itemName; // 아이템의 이름
    public Sprite itemImage; // 아이템의 이미지(인벤 토리 안에서 띄울)
    //public GameObject itemPrefab;  // 아이템의 프리팹 (아이템 생성시 프리팹으로 찍어냄)

    public int minScore;  // 스코어
    public int maxScore;
    public string BGM;
    public Sprite mapChangeEffect;
    public int mapChangeEffectTime;
    public int bonus;
}