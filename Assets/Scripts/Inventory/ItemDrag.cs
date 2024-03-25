using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDrag : MonoBehaviour,IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    private InventoryController inventoryController;
    Image image;
    ItemDescription itemDesription;
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
            inventoryController.LeftMouseButtonPress();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)//오른쪽 눌렀을 때
        {
            if(inventoryController.SelectedItemGrid == inventoryController.playerInventoryGrid)
                inventoryController.SellItemButton();
        }
    }

    public void OnPointerUp(PointerEventData eventData)//손을 뗐을 때 실행
    {
        if (eventData.button == PointerEventData.InputButton.Left)//마우스 왼쪽 클릭일 때만
        {
            image.raycastTarget = true;
            Debug.Log("Click Up");
            inventoryController.LeftMouseButtonPut();
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemDesription == null)
        {
            itemDesription = InventoryController.Instance.itemDescriptionUI.GetComponent<ItemDescription>();
        }
        itemDesription.currentItem = GetComponent<InventoryItem>();
        itemDesription.gameObject.SetActive(true);
        itemDesription.SetTransform();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (itemDesription != null)
        { 
            itemDesription.gameObject.SetActive(false);
            itemDesription.currentItem = null;
        }
    }
    private void OnDestroy()//삭제될 때 실행
    {
        if (itemDesription != null)
        { 
            itemDesription.gameObject.SetActive(false);
            itemDesription.currentItem = null;
        }
    }
}
