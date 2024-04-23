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
    // Start is called before the first frame update

    //private void Update()
    //{
    //    currentCount = storage.currentCount;
    //    maxCount = storage.maxCount;
    //    remainCount = maxCount - currentCount;
    //    countText.text = string.Format($"{remainCount}");
    //}
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
        Debug.Log("init");
    }
    private void OnDestroy()
    {
        storage.currentCountEvent -= CountUpdate;
        Debug.Log("destroy");
    }
}
