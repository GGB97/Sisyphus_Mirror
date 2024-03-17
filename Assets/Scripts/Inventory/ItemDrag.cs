using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour,IPointerDownHandler, IBeginDragHandler,IDragHandler, IPointerUpHandler
{
    private InventoryController inventoryController;
    private void Awake()
    {
        inventoryController = InventoryController.Instance;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //inventoryController.startPosition = transform.position; ;//시작 위치 지정
        //Vector2 mousePostion = Input.mousePosition;
        //inventoryController.LeftMouseButtonPress(mousePostion);
    }
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Drag");
        //transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("Drag End");
        //inventoryController.LeftMouseButtonPut(transform.position);
    }

    //public void OnPointerClick(PointerEventData eventData)
    //{
        
    //}

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Click");
        inventoryController.startPosition = transform.position; ;//시작 위치 지정
        Vector2 mousePostion = Input.mousePosition;
        inventoryController.LeftMouseButtonPress(mousePostion);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Click Up");
        inventoryController.LeftMouseButtonPut(transform.position);
    }
}
