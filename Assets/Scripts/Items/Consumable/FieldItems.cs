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
                    PlayerGetGold();
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


    public void SetValue(int index)
    {
        _value = index;
    }

    void PlayerGetGold()
    {
        //SetValue();
        //Debug.Log($"Player Get '{_value}' Gold");
        GameManager.Instance.Player.ChangeGold( _value );
        GameManager.Instance.totalGold += _value;
        
        gameObject.SetActive(false);
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
