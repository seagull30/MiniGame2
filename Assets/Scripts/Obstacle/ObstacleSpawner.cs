using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    private ObjectItem objectItem;
    private bool _spawnerActive;

    public float _timer = 0f;
    public float randomTime = 0f;
    private Vector2 _poolPosition;
    [SerializeField]
    private float _interval = 20;

    private Queue<ObjectMotion> _obstacles = new Queue<ObjectMotion>();
    private GameObject _obstacle;


    private void Awake()
    {
        objectItem = Resources.Load<ObjectItem>($"ScriptableData/Object/{gameObject.name}");
    }
    private void Start()
    {
        //랜덤 시간마다 생성되게 설정하기
        randomTime = Random.Range(objectItem.minSpawnTime, objectItem.maxSpawnTime);
        Initialize(10);
    }

    private void Update()
    {
        checkactive();
        Fire();
    }

    private void Initialize(int initCount)
    {
        for (int i = 0; i < initCount; i++)
        {
            _obstacles.Enqueue(CreateNewObject());
        }
    }

    private ObjectMotion CreateNewObject()
    {
        var newObj = Instantiate(objectItem.itemPrefab).GetComponent<ObjectMotion>();
        newObj.Init(objectItem.itemName, objectItem.moveSpeed);
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public void GetObject(Vector2 _poolPosition)
    {
        if (_obstacles.Count > 0)
        {
            var obj = _obstacles.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            obj.ParentScript = this;
            obj.transform.position = _poolPosition;
            //obj.transform.LookAt(GameManager.Instance.player.transform);
        }
        else
        {
            var newObj = CreateNewObject();
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            newObj.ParentScript = this;
            newObj.transform.position = _poolPosition;
            //newObj.transform.LookAt(GameManager.Instance.player.transform);
        }
    }

    public void ReturnObject(ObjectMotion obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(transform);
        _obstacles.Enqueue(obj);
    }


    private void checkactive()
    {
        if (GameManager.Instance.Score > objectItem.minScore && GameManager.Instance.Score < objectItem.maxScore)
        {
            //생산함
            _spawnerActive = true;
        }
        else
        {
            //오브젝트 생산안함
            _spawnerActive = false;
        }
    }

    private void Fire()
    {
        if (_spawnerActive)
        {
            //시간이 랜덤 시간 이상이 되면 생성
            if (_timer > randomTime)
            {
                _timer = 0f;
                randomTime = Random.Range((float)objectItem.minSpawnTime, (float)objectItem.maxSpawnTime);


                //-2 ~ 5사이 랜덤값 출력
                int randomPos = Random.Range(0, 7);

                if (objectItem.itemName.Contains("Left"))
                {
                    _poolPosition = new Vector2(_interval, randomPos);
                }
                else
                {
                    _poolPosition = new Vector2(-_interval, randomPos);
                }

                //오브젝트 생성
                GetObject(_poolPosition);
                //GameObject child = Instantiate(objectItem.itemPrefab, _poolPosition, Quaternion.identity);
                //child.transform.SetParent(this.transform);
            }
            else
            {
                _timer += Time.deltaTime;
            }
        }
    }



}
