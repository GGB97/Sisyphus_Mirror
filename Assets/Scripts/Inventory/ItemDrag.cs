using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDrag : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
{
    private InventoryController inventoryController;
    Image image;
    private void Awake()
    {
        inventoryController = InventoryController.Instance;
        image = GetComponent<Image>();
    }
    public void OnPointerDown(PointerEventData eventData) //마우스를 누른 순간 실행
    {
        if (eventData.button == PointerEventData.InputButton.Left)//마우스 왼쪽 클릭일 때만
        {
            Debug.Log("Click Down");
            image.raycastTarget = false;
            inventoryController.startPosition = transform.position; ;//시작 위치 지정
            Vector2 mousePostion = Input.mousePosition;
            inventoryController.LeftMouseButtonPress(mousePostion);
        }
    }

    public void OnPointerUp(PointerEventData eventData)//손을 뗐을 때 실행
    {
        if (eventData.button == PointerEventData.InputButton.Left)//마우스 왼쪽 클릭일 때만
        {
            image.raycastTarget = true;
            Debug.Log("Click Up");
            inventoryController.LeftMouseButtonPut(transform.position);
        }
    }
}
