using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Storage : ItemGrid
{
    //public Dictionary<ItemType, List<InventoryItem>> storage = new Dictionary<ItemType, List<InventoryItem>>();//창고 아이템
  
    protected override void Start()
    {
        SetGridSize(4, 4);//그리드 4,4설정
        base.Start();
        CreateBaseBlock(4,4);
        maxCount = 2;
    }
    public void CreateBaseBlock(int gridWidth, int gridHeight)//중앙에 block 추가
    {
        Image image = null;
        for (int x = 0; x < gridWidth; x++)//중앙 바꾸기
        {
            for (int y = 0; y < gridHeight; y++)
            {
                image = panelSlots[x,y].GetComponent<Image>();
                image.enabled = false;
                panelSlots[x, y].ChangeSlotState(PanelSlotState.Empty);
            }
        }
    }
}
