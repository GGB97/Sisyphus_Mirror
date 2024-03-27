using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InventoryItem currentItem;
    public RectTransform rectTransform;
    public GameObject RightClickPanel;

    public bool isHovering = false;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void SetTransform(float x = 0f, float y = 0f)
    {
        RectTransform newtransform = currentItem.gameObject.GetComponent<RectTransform>();
        float posX = ItemGrid.TileSizeWidth * currentItem.itemSO.IconWidth / 2;
        float posY = ItemGrid.TileSizeHeight * currentItem.itemSO.IconHeight / 2;
        rectTransform.position = newtransform.position + new Vector3(x + posX, y + posY, 0);
    }
    public void SetCurrentItemNull()
    {
        currentItem = null;
    }
    public void RightClick()//아이템 위에서 우클릭 했을 시 실행
    {
        RightClickPanel.SetActive(true);//패널 ON
        transform.SetParent(InventoryController.Instance.canvasTransform);//
        transform.SetAsLastSibling();
    }
    public void ExitExplnationUI()//우클릭 UI를 끌 때 실행
    {
        RightClickPanel.SetActive(false);
        if (currentItem == null)//아이템이 삭제된 후에 실행됐을 때
        {
            
        }
        else
        {
            ItemDrag itemDrag = currentItem.GetComponent<ItemDrag>();
            if (itemDrag != null)
            {
                itemDrag.isPressed = false;
                itemDrag.ExitUI();
            }
        } 
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        //transform.SetParent(InventoryController.Instance.canvasTransform);//호버되면 독립적인 객체로 존재
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        //ItemDrag itemDrag = currentItem.GetComponent<ItemDrag>();
        //if (itemDrag != null)
        //{
        //    itemDrag.ExitUI();
        //}
    }
    public void ClickSellItemButton()
    {
        InventoryController.Instance.SellItemButton(currentItem);
    }
}
