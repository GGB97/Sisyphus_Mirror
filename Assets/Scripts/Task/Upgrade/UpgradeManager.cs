using System.Collections;
using System.Collections.Generic;
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
    UpgradeUI _upgradeUI;

    private void Awake()
    {
        _player = GameManager.Instance.Player;
    }

    public void OnpenUI()
    {
        if (_upgradeUI == null)
        {
            _upgradeUI = Instantiate(Resources.Load<UpgradeUI>("Task/Upgrade/PlayerUpgradeCanvas"));
        }
        else if (_upgradeUI.enabled == false)
        {
            _upgradeUI.enabled = true;
        }

        _upgradeUI.UpdateSlots();
    }

    public void CloseUI()
    {
        if (_upgradeUI == null)
            return;

        _upgradeUI.enabled = false;
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
}
