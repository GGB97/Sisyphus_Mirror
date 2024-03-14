using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PanelSlotState
{
    Empty,
    Full,
    Add,
    Null,
}
public class PanelSlot : MonoBehaviour
{
    private PanelSlotState state = PanelSlotState.Null;
    Image image;
    public int posX;
    public int posY;
    //private InventoryManager _inventoryManager;

    // Start is called before the first frame update
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    void Start()
    {
        //_inventoryManager = InventoryManager.Instance;
        //image.sprite = _inventoryManager.slotSprites[(int)PanelSlotState.Null];
    }
    public void ChangeSlotState(PanelSlotState changeState)
    {
        state = changeState;//상태 변경
        ChangeSlotSprite();
    }
    //public bool CanAddSlot(int posX , int posY)
    //{
    //    if()
    //    return true;
    //}
    private void ChangeSlotSprite()//상태에 맞는 스프라이트 변경
    {
        switch (state) 
        {
            case PanelSlotState.Empty:
                image.sprite = InventoryManager.Instance.slotSprites[(int)PanelSlotState.Empty];
                break;
            case PanelSlotState.Full:
                image.sprite = InventoryManager.Instance.slotSprites[(int)PanelSlotState.Full];
                break;
            case PanelSlotState.Add:
                image.sprite = InventoryManager.Instance.slotSprites[(int)PanelSlotState.Add];
                break;
            case PanelSlotState.Null:
                image.sprite = InventoryManager.Instance.slotSprites[(int)PanelSlotState.Null];
                break;
            default:
                {
                    image.sprite = InventoryManager.Instance.slotSprites[(int)PanelSlotState.Null];
                    break;
                }
        }
    }
    public bool CompareState(PanelSlotState compareState)
    {
        return state == compareState ? true : false;
    }
    public void SetPosition(int x , int y)
    {
        posX = x;
        posY = y;
    }
}
