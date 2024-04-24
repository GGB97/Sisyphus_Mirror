using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescription : MonoBehaviour
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
    public Button useButton;

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
        float distance;

        posX = ItemGrid.TileSizeWidth * currentItem.WIDTH / 2;
        posY = ItemGrid.TileSizeHeight * currentItem.HEIGHT / 2;
        Vector2 newPos = newtransform.position + new Vector3(posX + x, posY + y, 0);
        distance = newPos.y - rectTransform.sizeDelta.y; //좌하단의 좌표
        if (distance < 0)
            newPos += new Vector2(0, -distance);

        rectTransform.position = newPos;
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
            if (currentItem.itemSO.Price == 0)//가격이 0이면 버튼 비활성화
            {
                sellButton.gameObject.SetActive(false);
                putOutSideButton.gameObject.SetActive(false);
            }
            else
            {
                sellButton.gameObject.SetActive(true);
                putOutSideButton.gameObject.SetActive(true);
            }

            if (currentItem.itemSO.ItemType == ItemType.Weapon)//아이템 종류가 무기일 때만 combine 버튼 활성화 
            {
                combineButton.gameObject.SetActive(true);
                if (DataBase.Weapon.CheckItemId(currentItem.itemSO.Id + 1) == true && inventoryController.CheckUpgradableItem(currentItem.itemSO.Id) == true)
                {
                    combineButton.interactable = true;
                }
                else
                {
                    combineButton.interactable = false;
                }
            }
            else
            {
                combineButton.gameObject.SetActive(false);
            }

            if (currentItem.itemSO.ItemType == ItemType.Consumable)
            {
                useButton.gameObject.SetActive(true);
            }
            else
            {
                useButton.gameObject.SetActive(false);
            }

            if (putOutSideButton.gameObject.activeSelf == true)//빼두기가 활성화 되어 있을 때
            {
                if (inventoryController.CheckInventoryToStorage(currentItem) == true)
                {
                    putOutSideButton.interactable = true;
                }
                else
                {
                    putOutSideButton.interactable = false;
                }
            }

            buttonPanel.SetActive(true);
        }

        SetDescriptionText();//설명 적기
    }
    public void ClickSellItemButton()//아이템 판매 버튼
    {
        inventoryController.SellItemButton(currentItem);//아이템 판매
        SoundManager.Instance.PlayAudioClip("Purchase");
    }
    public void ClickPutOutSideButton()//빼두기 버튼
    {
        inventoryController.MoveInventoryToStorage(currentItem);//인벤토리에서 창고로 이동
        ExitExplnationUI();//툴팁 닫기
    }
    public void ClickCombineButton()//업그레이드 버튼
    {
        inventoryController.CombineWeaponItem(currentItem);
        ExitExplnationUI();//툴팁 닫기
    }
    public void ClickUseButton()//사용 버튼
    {
        inventoryController.UseConsumableItem(currentItem);
        SoundManager.Instance.PlayAudioClip("Drink");
        ExitExplnationUI();
    }
}
