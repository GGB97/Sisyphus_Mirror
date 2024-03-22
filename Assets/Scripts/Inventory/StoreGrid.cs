using System;
using UnityEngine.UI;

public class StoreGrid : ItemGrid
{
    public InventoryItem currentStoreItem;
    int gridWidth = 4;
    int gridHeight = 4;

    protected override void Start()
    {
        SetGridSize(gridWidth, gridHeight);//그리드 4,4설정
        base.Start();
        CreateBaseBlock(gridWidth, gridHeight);

        InventoryController.Instance.OnStoreReroll();
    }
    public void CreateBaseBlock(int gridWidth, int gridHeight)//중앙에 block 추가
    {
        Image image = null;
        for (int x = 0; x < gridWidth; x++)//중앙 바꾸기
        {
            for (int y = 0; y < gridHeight; y++)
            {
                image = panelSlots[x, y].GetComponent<Image>();
                
                panelSlots[x, y].ChangeSlotState(PanelSlotState.Empty);
            }
        }
    }

    public void AddStoreStock(InventoryItem item)
    {
        currentStoreItem = item;
        Image image = null;
        for (int x = 0; x < gridWidth; x++)//중앙 바꾸기
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (panelSlots[x, y].CompareState(PanelSlotState.Empty))
                {
                    image = panelSlots[x, y].GetComponent<Image>();
                    image.color = new UnityEngine.Color(0, 0, 0, 0f);
                }
            }
        }
        image = inventoryPanel.GetComponent<Image>();
        image.color = new UnityEngine.Color(0, 0, 0, 0f);
    }

    public void ResetPanelStates()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                panelSlots[x, y].ChangeSlotState(PanelSlotState.Empty);
            }
        }
        Array.Clear(inventoryItemSlot, 0, gridSizeWidth);
        Destroy(currentStoreItem.gameObject);
    }
}
