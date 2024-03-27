using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreGrid : ItemGrid
{
    public List<InventoryItem> currentStoreItem = new List<InventoryItem>();
    int gridWidth = 10;
    int gridHeight = 10;

    protected override void Start()
    {
        SetGridSize(gridWidth, gridHeight);
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
                image.color = new Color(0, 0, 0, 0f);

                panelSlots[x, y].ChangeSlotState(PanelSlotState.Empty);
            }
        }
    }

    public void AddStoreStock(InventoryItem item)
    {
        currentStoreItem.Add(item);

        foreach (var items in currentStoreItem)
        {
            Debug.Log(items.name);
        }

        if (panelSlots[0, 0].CompareState(PanelSlotState.Empty))
            PlaceItem(item, 0, 0);
        else if (panelSlots[0, 7].CompareState(PanelSlotState.Empty))
            PlaceItem(item, 0, 7);
        else if (panelSlots[3, 3].CompareState(PanelSlotState.Empty))
            PlaceItem(item, 3, 3);
        else if (panelSlots[7, 0].CompareState(PanelSlotState.Empty))
            PlaceItem(item, 7, 0);
        else PlaceItem(item, 7, 7);
    }

    public void ClearEmptySolts()
    {
        Image image = null;

        for (int x = 0; x < gridWidth; x++)//중앙 바꾸기
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (panelSlots[x, y].CompareState(PanelSlotState.Empty))
                {
                    image = panelSlots[x, y].GetComponent<Image>();
                    image.color = new Color(0, 0, 0, 0f);
                }
            }
        }

        image = inventoryPanel.GetComponent<Image>();
        image.color = new Color(0, 0, 0, 0f);
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
        
        foreach(var item in currentStoreItem)
        {
            if(item == null) continue;
            Destroy(item.gameObject);
        }
        currentStoreItem.Clear();
    }
}
