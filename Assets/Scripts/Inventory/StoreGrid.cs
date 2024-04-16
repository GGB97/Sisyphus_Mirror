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
        int currentStage = DungeonManager.Instance.currnetstage == 0 ? 1 : DungeonManager.Instance.currnetstage;
        int price = 0;

        if (currentStage == 1)
        {
            price = item.itemSO.Price;
        }
        else price = Mathf.FloorToInt(item.itemSO.Price + 1.1f * currentStage);

        if (panelSlots[1, 1].CompareState(PanelSlotState.Empty))
        {
            PlaceItem(item, 1, 1);
            if(InventoryController.Instance.itemCost[0].transform.parent.gameObject.activeSelf == false)
                InventoryController.Instance.itemCost[0].transform.parent.gameObject.SetActive(true);
            InventoryController.Instance.itemCost[0].transform.parent.position = SetTransform(item);
            InventoryController.Instance.itemCost[0].text = price.ToString() + "<color=yellow> G</color>";
        }
        else if (panelSlots[1, 7].CompareState(PanelSlotState.Empty))
        {
            PlaceItem(item, 1, 7);
            if (InventoryController.Instance.itemCost[1].transform.parent.gameObject.activeSelf == false)
                InventoryController.Instance.itemCost[1].transform.parent.gameObject.SetActive(true);
            InventoryController.Instance.itemCost[1].transform.parent.position = SetTransform(item);
            InventoryController.Instance.itemCost[1].text = price.ToString() + "<color=yellow> G</color>";
        }
        else if (panelSlots[4, 4].CompareState(PanelSlotState.Empty))
        {
            PlaceItem(item, 4, 4);
            if (InventoryController.Instance.itemCost[2].transform.parent.gameObject.activeSelf == false)
                InventoryController.Instance.itemCost[2].transform.parent.gameObject.SetActive(true);
            InventoryController.Instance.itemCost[2].transform.parent.position = SetTransform(item);
            InventoryController.Instance.itemCost[2].text = price.ToString() + "<color=yellow> G</color>";
        }
        else if (panelSlots[7, 1].CompareState(PanelSlotState.Empty))
        {
            PlaceItem(item, 7, 1);
            if (InventoryController.Instance.itemCost[3].transform.parent.gameObject.activeSelf == false)
                InventoryController.Instance.itemCost[3].transform.parent.gameObject.SetActive(true);
            InventoryController.Instance.itemCost[3].transform.parent.position = SetTransform(item);
            InventoryController.Instance.itemCost[3].text = price.ToString() + "<color=yellow> G</color>";
        }
        else
        {
            PlaceItem(item, 7, 7);
            if (InventoryController.Instance.itemCost[4].transform.parent.gameObject.activeSelf == false)
                InventoryController.Instance.itemCost[4].transform.parent.gameObject.SetActive(true);
            InventoryController.Instance.itemCost[4].transform.parent.position = SetTransform(item);
            InventoryController.Instance.itemCost[4].text = price.ToString() + "<color=yellow> G</color>";
        }
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
        if (panelSlots == null || panelSlots.Length == 0) panelSlots = new PanelSlot[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (panelSlots[x, y] == null) panelSlots[x, y] = new PanelSlot();
                else panelSlots[x, y].ChangeSlotState(PanelSlotState.Empty);
            }
        }
        Array.Clear(inventoryItemSlot, 0, inventoryItemSlot.Length);

        foreach (var item in currentStoreItem)
        {
            if (item == null) continue;
            Destroy(item.gameObject);
        }
        currentStoreItem.Clear();
    }

    public Vector3 SetTransform(InventoryItem item)
    {
        RectTransform newtransform = item.gameObject.GetComponent<RectTransform>();
        float posX = TileSizeWidth * item.itemSO.IconWidth / 4;
        float posY = TileSizeHeight * item.itemSO.IconHeight / 2;
        return newtransform.position + new Vector3(posX - (TileSizeWidth * item.itemSO.IconWidth / 4), posY - TileSizeHeight * item.itemSO.IconHeight - TileSizeHeight, 0);
    }
    public void PannelImageClear(Vector2Int itemPos, InventoryItem currentItem)
    {
        for (int x = 0; x < currentItem.WIDTH; x++)
        {
            for (int y = 0; y < currentItem.HEIGHT; y++)
            {
                panelSlots[itemPos.x + x, itemPos.y + y].InvinsiblePannelImage();
            }
        }
    }
}
