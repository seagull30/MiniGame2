using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private ObjectItem objectItem;

    public float _timer = 0f;
    public float randomTime = 0f;
    private Vector2 _poolPosition;

    private void Start()
    {
        //랜덤 시간마다 생성되게 설정하기
        randomTime = Random.Range(objectItem.minSpawnTime, objectItem.maxSpawnTime);
    }

    private void Update()
    {
        //시간이 랜덤 시간 이상이 되면 생성
        if(_timer > randomTime)
        {
            _timer = 0f;
            randomTime = Random.Range((float)objectItem.minSpawnTime, (float)objectItem.maxSpawnTime);


            //-2 ~ 5사이 랜덤값 출력
            int randomPos = Random.Range(-2, 5);
            _poolPosition = new Vector2(20, randomPos);

            if (objectItem.itemName.Contains("Left"))
            {
                _poolPosition = new Vector2(20, randomPos);
            }
            else
            {
                _poolPosition = new Vector2(-20, randomPos);
            }


                //오브젝트 생성
                GameObject child = Instantiate(objectItem.itemPrefab, _poolPosition, Quaternion.identity);
            child.transform.SetParent(this.transform);

        }
        else
        {
            _timer += Time.deltaTime;
        }

    }

}
