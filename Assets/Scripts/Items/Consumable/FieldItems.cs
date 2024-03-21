using UnityEngine;

public enum FieldItemType
{
    Heart,
    Shield
}

public class FieldItems : MonoBehaviour
{
    [SerializeField] FieldItemType _type;

    private void OnTriggerEnter(Collider other)
    {
        if(LayerData.Player == (1 << other.gameObject.layer | LayerData.Player))
        {
            switch (_type)
            {
                case FieldItemType.Heart:
                    // TODO : 플레이어 체력 회복하기
                    
                    break;
                case FieldItemType.Shield:
                    // TODO : 플레이어 무적?
                    break;
            }
        }
    }
}
