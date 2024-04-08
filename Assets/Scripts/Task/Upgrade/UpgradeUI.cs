using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] GameObject _content;
    UpgradeSlot_UI[] _slots;
    [SerializeField] Button _resetBtn;

    public UpgradeSlot_UI[] Slots 
    {
        get
        {
            if (_slots == null)
            {
                _slots = GetComponentsInChildren<UpgradeSlot_UI>();
            }
            return _slots;
        }
    }

    private void Awake()
    {
        _slots = GetComponentsInChildren<UpgradeSlot_UI>();
    }

    private void OnEnable()
    {
        transform.localPosition = new Vector3(-1345, 0f, 0f);
        transform.DOLocalMoveX(-577, 1f).SetEase(Ease.OutQuart);

        UpdateSlots();
    }

    private void OnDisable()
    {
        transform.localPosition = new Vector3(-1345, 0f, 0f);
    }

    private void Start()
    {
        _resetBtn.onClick.AddListener(() => 
        {
            UpgradeManager.Instance.Refund();
        });
    }

    public void CloseUI()
    {
        gameObject.transform.DOLocalMoveX(-1345, 0.5f).SetEase(Ease.OutQuart).OnComplete(() =>
        {
            if(transform.localPosition.x <= -1340)
                gameObject.SetActive(false);
        });
    }

    public void UpdateSlots()
    {
        foreach (var item in _slots)
        {
            UpgradeData data = DataBase.PlayerUpgrade.Get(item.id);

            item.UpdateUI(data);
        }
    }
}
