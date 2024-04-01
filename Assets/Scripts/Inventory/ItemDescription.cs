using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private InventoryController inventoryController;
    private Transform canvasTransform;
    public InventoryItem currentItem;
    public RectTransform rectTransform;
    public GameObject rightClickPanel;
    public GameObject buttonPanel;

    [SerializeField]
    private float sellPercent = 0.5f;

    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI gradeText;
    [SerializeField]
    private TextMeshProUGUI descriptionText;
    [SerializeField]
    private TextMeshProUGUI sellButtonText;

    public Button sellButton;
    public Button combineButton;
    public Button putOutSideButton;
    public Button exitButton;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        inventoryController = InventoryController.Instance;
        canvasTransform = inventoryController.canvasTransform;
    }
    public void SetTransform(float x = 0f, float y = 0f)//위치 설정
    {
        UISettingsAvailable();//각 상황에 맞는 UI 크기 조절
        Canvas.ForceUpdateCanvases();//정보 최신화
        RectTransform newtransform = currentItem.gameObject.GetComponent<RectTransform>();
        float posX;
        float posY;

        if (newtransform.position.y > 0.5f * Screen.height)//스크린 중간 보다 위에 있을 때
        {
            posX = ItemGrid.TileSizeWidth * currentItem.WIDTH / 2;
            posY = ItemGrid.TileSizeHeight * currentItem.HEIGHT / 2;
        }
        else
        {
            posX = ItemGrid.TileSizeWidth * currentItem.WIDTH / 2;
            posY = (-ItemGrid.TileSizeHeight * currentItem.HEIGHT / 2) + (rectTransform.sizeDelta.y);
        }
      
        rectTransform.position = newtransform.position + new Vector3(posX + x, posY + y, 0);
    }
    public void SetCurrentItemNull()//아이템 null 초기화
    {
        currentItem = null;
    }
    public void RightClick()//아이템 위에서 우클릭 했을 시 실행
    {
        rightClickPanel.SetActive(true);//패널 ON
        transform.SetParent(canvasTransform);//
        transform.SetAsLastSibling();
    }
    public void ExitExplnationUI()//우클릭 UI를 끌 때 실행
    {
        rightClickPanel.SetActive(false);
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
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //ItemDrag itemDrag = currentItem.GetComponent<ItemDrag>();
        //if (itemDrag != null)
        //{
        //    itemDrag.ExitUI();
        //}
    }
   
    private void SetDescriptionText()//설명 적기
    {
        nameText.text = currentItem.itemSO.Name;//이름 부분
        gradeText.text = string.Format($"등급{(int)currentItem.itemSO.Grade + 1}");
        gradeText.color = inventoryController.BlockColorDictionary[currentItem.itemSO.Grade].color;
        //gradeText.color = Utilities.HexColor(inventoryController.BlockColorDictionary[currentItem.itemSO.Grade].color);
        StringBuilder sb = currentItem.itemSO.SetExplantion(currentItem.itemSO);//설명 부분
        descriptionText.text = sb.ToString();//설명 부분

        if (buttonPanel.activeSelf == true)
            sellButtonText.text = string.Format($"팔기 : {(int)(currentItem.itemSO.Price * sellPercent)} G");
        sb = null; //null로 만들면서 메모리 해제 대상이 됨
    }
    public void UISettingsAvailable()//버튼 UI를 표시할지 정하고 설명 적기
    {
        ItemGrid currentGrid = inventoryController.SelectedItemGrid;
        if (currentGrid == inventoryController.storeGrid || currentGrid == inventoryController.storageGrid)
        {
            buttonPanel.SetActive(false);
        }
        else if (currentGrid == inventoryController.playerInventoryGrid) //플레이어 인벤토리일 경우
        {
            if (inventoryController.CheckInventoryToStorage(currentItem) == true)
            {
                putOutSideButton.interactable = true;
            }
            else
            {
                putOutSideButton.interactable = false;
            }
            buttonPanel.SetActive(true);
        }

        SetDescriptionText();//설명 적기
    }
    public void ClickSellItemButton()//아이템 판매 버튼
    {
        inventoryController.SellItemButton(currentItem);//아이템 판매
    }
    public void ClickPutOutSideButton()//빼두기 버튼
    {
        inventoryController.MoveInventoryToStorage(currentItem);//인벤토리에서 창고로 이동
        ExitExplnationUI();//툴팁 닫기
    }
    public void ClickCombineButton()//업그레이드 버튼
    {
        //inventoryController.
    }
}
