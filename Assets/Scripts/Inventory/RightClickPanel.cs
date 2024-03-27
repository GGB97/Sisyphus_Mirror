using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RightClickPanel : MonoBehaviour, IPointerDownHandler
{
    private InventoryController inventoryController;
    private ItemDescription itemDescription;

    private void Awake()
    {
        inventoryController = InventoryController.Instance;
    }
    private void OnEnable()
    {
        itemDescription = inventoryController.itemDescriptionUI.GetComponent<ItemDescription>();
    }
    public void OnPointerDown(PointerEventData eventData)//바닥을 클릭 했을 때
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (itemDescription == null)
                return;

            itemDescription.ExitExplnationUI();
        }
    }
}
