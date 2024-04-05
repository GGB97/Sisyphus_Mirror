using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerStatType
{
    Health,
    PhysicalAtk,
    MagicAtk,
    AtkSpeed,
    MoveSpeed,
    Def,
    CritRate,
    CritDamage,
    LifeSteal
}

public class RuneStoneUI : MonoBehaviour
{
    public int leftAttemptCount;
    int _selectedStatCounter = 0;

    [SerializeField] Image _selectedStatPanel;

    [SerializeField] GameObject _selectedStatPrefab;

    [SerializeField] Player _player;
    [SerializeField] Status _playerStatus;

    List<PlayerStatType> _selectedStatus = new List<PlayerStatType>();

    // Start is called before the first frame update
    void Start()
    {
        _player = GameManager.Instance.Player;
        _playerStatus = _player.currentStat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickStatPlusButton(int index)
    {
        if(_selectedStatCounter++ >= 6)
        {
            return;
        }

        GameObject go;
        go = Instantiate(_selectedStatPrefab, _selectedStatPanel.transform);
        _selectedStatus.Add((PlayerStatType)index);

        switch ((PlayerStatType)index)
        {
            case PlayerStatType.Health:
                go.GetComponentInChildren<TextMeshProUGUI>().text = "체력";
                break;
            case PlayerStatType.PhysicalAtk:
                go.GetComponentInChildren<TextMeshProUGUI>().text = "물리 공격력";
                break;
            case PlayerStatType.MagicAtk:
                go.GetComponentInChildren<TextMeshProUGUI>().text = "마법 공격력";
                break;
            case PlayerStatType.AtkSpeed:
                go.GetComponentInChildren<TextMeshProUGUI>().text = "공격 속도";
                break;
            case PlayerStatType.MoveSpeed:
                go.GetComponentInChildren<TextMeshProUGUI>().text = "이동 속도";
                break;
            case PlayerStatType.Def:
                go.GetComponentInChildren<TextMeshProUGUI>().text = "방어력";
                break;
            case PlayerStatType.CritRate:
                go.GetComponentInChildren<TextMeshProUGUI>().text = "치명타 확률";
                break;
            case PlayerStatType.CritDamage:
                go.GetComponentInChildren<TextMeshProUGUI>().text = "치명타 데미지";
                break;
            case PlayerStatType.LifeSteal:
                go.GetComponentInChildren<TextMeshProUGUI>().text = "피해 흡혈";
                break;
        }
    }

    public void OnClickExitButton()
    {
        gameObject.SetActive(false);
    }

    public void OnClickSelectedStatButton()
    {

    }
}
