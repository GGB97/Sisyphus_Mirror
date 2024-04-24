using TMPro;
using UnityEngine;

public class AvailableCount : MonoBehaviour
{
    Storage storage;
    int currentCount = 0;
    int maxCount = 0;
    int remainCount = 0;
    public TextMeshProUGUI countText;
    public void CountUpdate()
    {
        currentCount = storage.currentCount;
        maxCount = storage.maxCount;
        remainCount = maxCount - currentCount;
        if (remainCount > 0)//놓을 수 있을 때 흰 텍스트
            countText.text = string.Format($"<color=white>{remainCount}</color>");
        else
            countText.text = string.Format($"<color=red>{remainCount}</color>");
    }
    public void Init()
    {
        storage = InventoryController.Instance.storageGrid;
        storage.currentCountEvent += CountUpdate;
        CountUpdate();
    }
    private void OnDestroy()
    {
        storage.currentCountEvent -= CountUpdate;
    }
}
