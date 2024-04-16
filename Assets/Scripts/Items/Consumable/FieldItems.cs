using UnityEngine;

public enum FieldItemType
{
    Heart,
    Shield,
    Gold
}

public class FieldItems : MonoBehaviour
{
    DungeonManager _dm;

    [SerializeField] FieldItemType _type;
    int _value;

    public float moveSpeed = 20f;

    Player _player;
    private Transform playerPos;

    private void OnEnable()
    {
        if (_type != FieldItemType.Gold)
        {
            if (_dm == null)
            {
                _dm = DungeonManager.Instance;
            }
            _dm.OnStageClear += ActiveFalse;
        }
    }

    private void OnDisable()
    {
        if (_type != FieldItemType.Gold)
        {
            if (_dm != null)
            {
                _dm.OnStageClear -= ActiveFalse;
            }
        }

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
                    PlayerGetHeart();
                    break;
                case FieldItemType.Shield:
                    // TODO : 플레이어 무적?
                    PlayerGetShield();
                    break;
                case FieldItemType.Gold:
                    PlayerGetGold();
                    break;
            }
        }
    }

    private void Start()
    {
        _player = GameManager.Instance.Player;
        playerPos = _player.transform;
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
        _player.ChangeGold(_value);
        GameManager.Instance.totalGold += _value;

        gameObject.SetActive(false);
    }

    void PlayerGetHeart()
    {
        _player.HealthSystem.TakeHeal(_value, DamageType.Heal);
        _player.HealthChange();

        gameObject.SetActive(false);
    }

    void PlayerGetShield()
    {
        _player.currentStat.shield += _value;
        _player.InvokeShieldChange();

        gameObject.SetActive(false);
    }

    void Magnet()
    {
        float magnetDistance = GameManager.Instance.Player.magnetDistance;
        float distance = Vector3.Distance(transform.position, playerPos.position);

        if (distance <= magnetDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPos.position, moveSpeed * Time.deltaTime);
        }
    }

    void ActiveFalse()
    {
        gameObject.SetActive(false);
    }
}
