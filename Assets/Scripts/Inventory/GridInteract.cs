using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridInteract : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    InventoryController inventoryController;
    ItemGrid ItemGrid;

    void Start()
    {
        inventoryController = InventoryController.Instance;
        ItemGrid = GetComponent<ItemGrid>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        inventoryController.SelectedItemGrid = ItemGrid;
        //Debug.Log($"Enter Grid :{this.name}");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //inventoryController.SelectedItemGrid = null;
        //Debug.Log($"Exit Grid :{this.name}");
    }
}
