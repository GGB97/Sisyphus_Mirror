using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] GameObject _content;
    UpgradeSlot_UI[] slots;
    [SerializeField] Button _resetBtn;

    private void Awake()
    {
        slots = GetComponentsInChildren<UpgradeSlot_UI>();
    }

    private void Start()
    {
        UpdateSlots();

        _resetBtn.onClick.AddListener(ResetAll);
    }

    public void UpdateSlots()
    {
        foreach (var item in slots)
        {
            UpgradeData data = DataBase.PlayerUpgrade.Get(item.id);

            item.UpdateUI(data);
        }
    }

    public void ResetAll()
    {
        foreach (var item in slots)
        {
            UpgradeData data = DataBase.PlayerUpgrade.Get(item.id);

            item.Reset();
            item.UpdateUI(data);
        }
    }
}
