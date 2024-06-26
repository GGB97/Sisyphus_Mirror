using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : UI_Base
{
    UIManager _ui;

    [SerializeField] GameObject _content;
    UpgradeSlot_UI[] _slots;
    [SerializeField] Button _refundBtn;

    Tween _openTween;
    Tween _closeTween;

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
        _ui = UIManager.Instance;
        UpgradeManager.Instance.SetUpdate(this);
        _slots = GetComponentsInChildren<UpgradeSlot_UI>();
    }

    private void OnEnable()
    {
        _closeTween.Kill();

        UpdateSlots();
        transform.localPosition = new Vector3(-1345, 0f, 0f);
        _openTween = transform.DOLocalMoveX(-577, 1f).SetEase(Ease.OutQuart);

        _ui.AddActiveUI(gameObject);
    }

    private void OnDisable()
    {
        transform.localPosition = new Vector3(-1345, 0f, 0f);
        _ui.RemoveActiveUI(gameObject);
    }

    private void Start()
    {
        _refundBtn.onClick.AddListener(() =>
        {
            UpgradeManager.Instance.Refund();
        });
    }

    public override void CloseUI()
    {
        if (gameObject.activeSelf == false)
            return;

        _openTween.Kill();

        _closeTween = transform.DOLocalMoveX(-1345, 0.5f).SetEase(Ease.OutQuart).OnComplete(() =>
        {
            if (transform.localPosition.x <= -1340)
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
