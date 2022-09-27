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
        //���� �ð����� �����ǰ� �����ϱ�
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
            //������
            _spawnerActive = true;
        }
        else
        {
            //������Ʈ �������
            _spawnerActive = false;
        }
    }

    private void Fire()
    {
        if (_spawnerActive)
        {
            //�ð��� ���� �ð� �̻��� �Ǹ� ����
            if (_timer > randomTime)
            {
                _timer = 0f;
                randomTime = Random.Range((float)objectItem.minSpawnTime, (float)objectItem.maxSpawnTime);


                //-2 ~ 5���� ������ ���
                int randomPos = Random.Range(0, 7);

                if (objectItem.itemName.Contains("Left"))
                {
                    _poolPosition = new Vector2(_interval, randomPos);
                }
                else
                {
                    _poolPosition = new Vector2(-_interval, randomPos);
                }

                //������Ʈ ����
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
