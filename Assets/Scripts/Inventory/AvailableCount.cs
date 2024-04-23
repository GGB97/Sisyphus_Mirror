using System.Collections;
using System.Collections.Generic;
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
        countText.text = string.Format($"{remainCount}");
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
