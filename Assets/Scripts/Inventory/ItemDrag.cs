using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IBeginDragHandler,IDragHandler, IEndDragHandler
{
    private InventoryController inventoryController;
    private void Awake()
    {
        inventoryController = InventoryController.Instance;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        inventoryController.startPosition = transform.position; ;//시작 위치 지정
        inventoryController.LeftMouseButtonPress();
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        inventoryController.LeftMouseButtonPut(transform.position);
    }

}
