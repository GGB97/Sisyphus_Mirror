using UnityEngine;

public enum FieldItemType
{
    Heart,
    Shield,
    Gold
}

public class FieldItems : MonoBehaviour
{
    [SerializeField] FieldItemType _type;
    int _value;

    public float moveSpeed = 10f;
    public float magnetDistance;
    private Transform player;

    private void OnDisable()
    {
        FieldItemsPooler.Instance.ReturnToPull(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (LayerData.Player == (1 << other.gameObject.layer | LayerData.Player))
        {
            switch (_type)
            {
                case FieldItemType.Heart:   
                    // TODO : 플레이어 체력 회복하기

                    break;
                case FieldItemType.Shield:
                    // TODO : 플레이어 무적?
                    break;
                case FieldItemType.Gold:
                    PlayerGetGole();
                    Destroy(gameObject); // 이 아이템들도 오브젝트 풀로 관리?
                    break;
            }
        }
    }

    private void Start()
    {
        player = GameManager.Instance.Player.transform;
        magnetDistance = GameManager.Instance.Player.magnetDistance;
    }

    private void Update()
    {
        Magnet();
    }


    public void SetValue()
    {
        _value = Random.Range(1, 3);
    }

    void PlayerGetGole()
    {
        SetValue();
        Debug.Log($"Player Get '{_value}' Gold");
        GameManager.Instance.Player.Data.Gold += _value;
        GameManager.Instance.totalGold += _value;
    }

    void Magnet()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if(distance <= magnetDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }
}
