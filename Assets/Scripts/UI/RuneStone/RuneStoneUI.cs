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
    [SerializeField] int _tutorialId;

    int _selectedStatCounter = 0;
    DungeonManager _dungeonManager;
    [SerializeField] GameObject _runeStoneUI;

    [SerializeField] Image _selectedStatPanel;
    [SerializeField] GameObject _selectedStatPrefab;

    [Header("Upgrade Chances")]
    [SerializeField] TextMeshProUGUI _remainChanceText;
    int _remainChance = 1;

    [Header("Player")]
    [SerializeField] Player _player;
    [SerializeField] Status _playerStatus;
    [SerializeField] GameObject _playerStatusUI;

    List<PlayerStatType> _selectedStatus = new List<PlayerStatType>();
    List<Button> _selectedStatusButtons = new List<Button>();

    [Header("RuneStoneSlider")]
    [SerializeField] Slider _runeStoneSlider;
    [SerializeField] TextMeshProUGUI _runeStoneConfirmButtonText;
    [SerializeField] bool _enabled = false;
    [SerializeField] bool _isUpdate = false;
    public float sliderRate = 0.05f;

    [Header("ResultNotice")]
    [SerializeField] GameObject _content;    // 결과창을 띄울 Grid Layout
    [SerializeField] GameObject _noticePopup;
    [SerializeField] GameObject _noticeStatPrefab;
    List<string> _modifiedStats = new List<string>();
    List<float> _modifiedStatsValue = new List<float>();
    List<GameObject> _noticeStats = new List<GameObject>();
    [SerializeField] TextMeshProUGUI _resultText;

    bool _isTried = false;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameManager.Instance.Player;
        _playerStatus = _player.currentStat;
        _dungeonManager = DungeonManager.Instance;

        _remainChance = 0;

        _dungeonManager.OnClearUI += CheckCurrentStage;
        _playerStatusUI.GetComponent<RuneStonePlayerStat>().UpdateStatsPanel();

        _runeStoneUI.SetActive(false);

        //_runeStoneSlider.value = 0f;
        //_enabled = false;

        //_runStoneConfirmButtonText.text = "Start";
        //_remainChance = 1;
        //_remainChanceText.text = _remainChance.ToString() + ($"<color=\"yellow\"> 회</color>");
    }

    // Update is called once per frame
    void Update()
    {
        if (_enabled && _isUpdate)
        {
            _runeStoneSlider.value += (sliderRate * Time.deltaTime * 30);

            if (_runeStoneSlider.value >= _runeStoneSlider.maxValue)
            {
                //_runeStoneSlider.value = 1;
                sliderRate = -sliderRate;
            }
            if (_runeStoneSlider.value <= _runeStoneSlider.minValue)
            {
                //_runeStoneSlider.value = 0;
                sliderRate = -sliderRate;
            }
        }
    }

    void CheckCurrentStage(int stage)
    {
        if (stage % 5 == 0)
        {
            _remainChance++;
            _runeStoneUI.SetActive(true);

            _runeStoneSlider.value = 0f;
            _enabled = false;
            _isUpdate = false;
            _isTried = false;

            _runeStoneConfirmButtonText.text = "Start";

            _remainChanceText.text = _remainChance.ToString() + ($"<color=\"yellow\"> 회</color>");

            if (PlayerPrefs.GetInt("runestoneTutorialFlag") == 0 && DungeonManager.Instance.currnetstage != 0) TutorialManager.Instance.PopupTutorial(TutorialType.RuneStone, _tutorialId);
        }
    }

    public void OnClickStatPlusButton(int index)
    {
        if (_selectedStatCounter >= 6 || _selectedStatCounter >= _remainChance)
        {
            return;
        }

        _selectedStatCounter++;
        _remainChanceText.text = (_remainChance - _selectedStatCounter).ToString() + ($"<color=\"yellow\"> 회</color>");
        GameObject go = Instantiate(_selectedStatPrefab, _selectedStatPanel.transform);

        if (go.TryGetComponent<Button>(out Button _button))
        {
            _selectedStatusButtons.Add(_button);
            _button.onClick.AddListener(() => OnClickSelectedStatButton(_selectedStatusButtons.Count - 1));
        }

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

    // RuneStonUI창 닫기
    public void OnClickExitButton()
    {
        _selectedStatus.Clear();

        foreach (var button in _selectedStatusButtons)
        {
            button.onClick.RemoveListener(() => OnClickSelectedStatButton(_selectedStatusButtons.Count - 1));
            Destroy(button.gameObject);
        }
        _selectedStatusButtons.Clear();
        _selectedStatCounter = 0;

        _runeStoneUI.SetActive(false);
        InventoryStats.Instance.UpdateStatsPanel();
    }

    // 선택된 스탯들 눌러서 없애기
    public void OnClickSelectedStatButton(int index)
    {
        _selectedStatus.RemoveAt(index);

        _selectedStatusButtons[index].onClick.RemoveListener(() => OnClickSelectedStatButton(_selectedStatusButtons.Count - 1));
        Destroy(_selectedStatusButtons[index].gameObject);
        _selectedStatusButtons.RemoveAt(index);
        _selectedStatCounter--;

        _remainChanceText.text = (_remainChance - _selectedStatCounter).ToString() + ($"<color=\"yellow\"> 회</color>");
    }

    // 강화 시도
    public void OnClickConfirmButton()
    {
        if (_selectedStatus.Count == 0) return;
        if (_isTried)
        {
            OnClickExitButton();
            return;
        }

        _isUpdate = false;
        _enabled = _enabled == true ? false : true;
        if (_enabled)
        {
            _runeStoneConfirmButtonText.text = "Stop";
            _isUpdate = true;
            return;
        }
        else
        {
            _runeStoneConfirmButtonText.text = "Close";
            _remainChance -= _selectedStatCounter;
            _isTried = true;
        }

        int bonusMin = 0, bonusMax = 0;

        if (_runeStoneSlider.value >= 0.45f && _runeStoneSlider.value <= 0.55f)
        {
            _resultText.text = "<color=green>Excellent!</color>";

            bonusMin = 5;
            bonusMax = 7;
        }
        else if (_runeStoneSlider.value >= 0.33f && _runeStoneSlider.value <= 0.66f)
        {
            _resultText.text = "<color=yellow>Good!</color>";

            bonusMin = 1;
            bonusMax = 3;
        }
        else if (_runeStoneSlider.value >= 0.2f && _runeStoneSlider.value <= 0.8f)
        {
            _resultText.text = "<color=#FF7F00>Bad</color>";
            bonusMin = -1;
            bonusMax = 1;
        }
        else
        {
            _resultText.text = "<color=red>Miss</color>";
            bonusMin = -2;
            bonusMax = 0;
        }
        SetPlayerStat(bonusMin, bonusMax);
    }

    private void SetPlayerStat(int bonusMin, int bonusMax)
    {
        Status _modifier = new Status();
        _modifiedStats.Clear();
        _modifiedStatsValue.Clear();
        int bonus = 0;

        foreach (var stat in _selectedStatus)
        {
            bonus = UnityEngine.Random.Range(bonusMin, bonusMax + 1);
            switch (stat)
            {
                case PlayerStatType.Health:
                    _modifier.maxHealth += bonus;
                    SetModifiedStats("체력", _modifier.maxHealth);
                    break;
                case PlayerStatType.PhysicalAtk:
                    _modifier.physicalAtk += bonus;
                    SetModifiedStats("물리 공격력", _modifier.physicalAtk);
                    break;
                case PlayerStatType.MagicAtk:
                    _modifier.magicAtk += bonus;
                    SetModifiedStats("마법 공격력", _modifier.magicAtk);
                    break;
                case PlayerStatType.AtkSpeed:
                    _modifier.attackSpeed += bonus;
                    SetModifiedStats("공격 속도", _modifier.attackSpeed);
                    break;
                case PlayerStatType.MoveSpeed:
                    _modifier.moveSpeed += bonus;
                    SetModifiedStats("이동 속도", _modifier.moveSpeed);
                    break;
                case PlayerStatType.Def:
                    _modifier.def += bonus;
                    SetModifiedStats("방어력", _modifier.def);
                    break;
                case PlayerStatType.CritRate:
                    _modifier.critRate += bonus;
                    SetModifiedStats("치명타 확률", _modifier.critRate);
                    break;
                case PlayerStatType.CritDamage:
                    _modifier.critDamage += bonus;
                    SetModifiedStats("치명타 데미지", _modifier.critDamage);
                    break;
                case PlayerStatType.LifeSteal:
                    _modifier.lifeSteal += bonus;
                    SetModifiedStats("피해 흡혈", _modifier.lifeSteal);
                    break;
            }
        }
        _playerStatus.InitStatus(_playerStatus, _modifier, true);
        if (_playerStatusUI.TryGetComponent<RuneStonePlayerStat>(out RuneStonePlayerStat playerStats))
        {
            playerStats.UpdateStatsPanel();
        }

        // TODO : 강화 결과 알려주기
        _noticePopup.gameObject.SetActive(true);

        for(int i = 0; i < _modifiedStats.Count; i++)
        {
            GameObject go = Instantiate(_noticeStatPrefab, _content.transform);
            go.GetComponent<NoticeStat>().SetNoticeStatText(_modifiedStats[i], _modifiedStatsValue[i].ToString("n2"));
            _noticeStats.Add(go);
        }
    }

    void SetModifiedStats(string type, float value)
    {
        _modifiedStats.Add(type);
        _modifiedStatsValue.Add(value);
    }

    public void OnClickNoticeCloseButton()
    {
        foreach(var go in _noticeStats)
        {
            Destroy(go);
        }
        _noticeStats.Clear();
        _noticePopup.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        //DungeonManager.Instance.OnStageEnd -= CheckCurrentStage;
        _dungeonManager.OnClearUI -= CheckCurrentStage;
    }
}
