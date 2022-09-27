using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField]
    private ObjectItem objectItem;

    public float _timer = 0f;
    public float randomTime = 0f;
    private Vector2 _poolPosition;

    private void Awake()
    {
        objectItem = Resources.Load<ObjectItem>($"ScriptableData/Object/{gameObject.name}");
    }
    private void Start()
    {
        //Debug.Log("Assets/Resources/ScriptableData/Object/" + gameObject.name);

        //���� �ð����� �����ǰ� �����ϱ�
        randomTime = Random.Range(objectItem.minSpawnTime, objectItem.maxSpawnTime);
    }

    private void OnEnable()
    {
    }

    private void Update()
    {
        //�ð��� ���� �ð� �̻��� �Ǹ� ����
        if (_timer > randomTime)
        {
            _timer = 0f;
            randomTime = Random.Range((float)objectItem.minSpawnTime, (float)objectItem.maxSpawnTime);


            //-2 ~ 5���� ������ ���
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


            //������Ʈ ����
            GameObject child = Instantiate(objectItem.itemPrefab, _poolPosition, Quaternion.identity);
            child.transform.SetParent(this.transform);

        }
        else
        {
            _timer += Time.deltaTime;
        }

    }

}