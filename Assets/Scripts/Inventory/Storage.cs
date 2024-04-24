using Unity.VisualScripting;

public class Storage : ItemGrid
{
    //public Dictionary<ItemType, List<InventoryItem>> storage = new Dictionary<ItemType, List<InventoryItem>>();//창고 아이템
    
    public AvailableCount availableCount;
    protected override void Start()
    {
        base.Start();
        CreateBaseBlock(gridSizeWidth, gridSizeWidth);
        maxCount = 2;
        availableCount.Init();
    }
    public void CreateBaseBlock(int gridWidth, int gridHeight)//중앙에 block 변경
    {
        //Image image = null;
        for (int x = 0; x < gridWidth; x++)//중앙 바꾸기
        {
            for (int y = 0; y < gridHeight; y++)
            {
                //image = panelSlots[x,y].GetComponent<Image>();
                //image.enabled = false;
                panelSlots[x, y].ChangeSlotState(PanelSlotState.Empty);
            }
        }
    }
}
