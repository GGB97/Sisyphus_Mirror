using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    static UpgradeManager _instance;
    public static UpgradeManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject();
                go.AddComponent<UpgradeManager>();
                go.name = "UpgradeManager";
                _instance = go.GetComponent<UpgradeManager>();
            }

            return _instance;
        }
    }

    Player _player;
    [SerializeField] UpgradeUI _upgradeUI;

    private void Awake()
    {
        _player = GameManager.Instance.Player;
    }

    public void SetUpdate(UpgradeUI ui)
    {
        _upgradeUI = ui;
    }

    public void OnpenUI()
    {
        if (_upgradeUI == null)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("Task/Upgrade/PlayerUpgradeCanvas"));
            _upgradeUI = go.GetComponentInChildren<UpgradeUI>();
        }
        else if (_upgradeUI.gameObject.activeSelf == false)
        {
            _upgradeUI.gameObject.SetActive(true);
        }
    }

    public void CloseUI()
    {
        if (_upgradeUI == null)
            return;

        _upgradeUI.CloseUI();
    }

    public bool Upgrade(UpgradeSlot_UI slot)
    {
        UpgradeData data = DataBase.PlayerUpgrade.Get(slot.id);

        data.Init();

        // 지금 최대 강화상태가 아니라면.
        if (data.CurrentGrade < data.MaxGrade)
        {
            if (_player.rune >= data.Count)
            {
                // 업그레이드 가능
                PlayerPrefs.SetInt($"Upgrade_{data.UpgradeType}", data.CurrentGrade + 1); // 데이터 저장 구현시 변경
                _player.ChangeRune(-data.Count);

                Debug.Log($"Upgrade (current : {data.CurrentGrade})");

                slot.UpdateUI(data);

                return true;
            }
            else
            {
                // 업그레이드 불가
                Debug.Log($"Can't Upgrade / current : {data.CurrentGrade}");

                return false;
            }
        }
        else
        {
            // 업그레이드 불가
            Debug.Log($"Can't Upgrade / current : {data.CurrentGrade}");

            return false;
        }

    }

    public void Refund()
    {
        UpgradeSlot_UI[] slots = _upgradeUI.Slots;

        foreach (var item in slots)
        {
            UpgradeData data = DataBase.PlayerUpgrade.Get(item.id);

            int cost = 0;
            for (int i = 0; i <= data.CurrentGrade; i++)
            {
                cost += data.CostModifiers[i].Cost;
            }
            GameManager.Instance.Player.ChangeRune(cost);

            data.Reset();
            item.UpdateUI(data);
        }
    }
}
