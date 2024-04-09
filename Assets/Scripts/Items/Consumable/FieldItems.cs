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
    float _value;

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

    public void SetValue(float value)
    {
        _value = value;
    }

    void PlayerGetGole()
    {
        Debug.Log($"Player Get '{_value}' Gold");
    }
}
