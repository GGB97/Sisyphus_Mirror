using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image _healthBar;
    [SerializeField] TextMeshProUGUI _healthText;
    [SerializeField] Image _expBar;
    [SerializeField] TextMeshProUGUI _shieldText;

    Player _player;
    InventoryController _inventoryController;

    void Start()
    {
        _player = GameManager.Instance.Player; //GameObject.FindWithTag("Player").GetComponent<Player>();
        _inventoryController = InventoryController.Instance;
        _inventoryController.nextStage += SetHealthBar;
        _player.PlayerHealthChange += UpdateHealthBar;
        _healthText.text = $"{_player.currentStat.health} / {_player.currentStat.maxHealth}";
        _player.PlayerExpChange += UpdateExpBar;

        _player.PlayerSheildChange += UpdateShield;
    }

    void SetHealthBar()
    {
        UnityEngine.Debug.Log($"체력바 세팅. 현재 체력 : {_player.currentStat.health}");
        UnityEngine.Debug.Log($"체력바 세팅. 최대 체력 : {_player.currentStat.maxHealth}");
        _healthText.text = $"{(int)_player.currentStat.health} / {_player.currentStat.maxHealth}";
        _healthBar.fillAmount = _player.currentStat.health / _player.currentStat.maxHealth;
    }

    void UpdateHealthBar(float maxHealth, float health)
    {
        string callingFuncName = new StackFrame(1).GetMethod().Name;
        UnityEngine.Debug.Log(callingFuncName);

        UnityEngine.Debug.Log($"체력바 업데이트. 현재 체력 : {_player.currentStat.health}");
        UnityEngine.Debug.Log($"체력바 업데이트. 최대 체력 : {_player.currentStat.maxHealth}");
        _healthText.text = $"{(int)health} / {maxHealth}";
        _healthBar.fillAmount = health / maxHealth;
    }

    void UpdateExpBar(float exp, float maxExp)
    {
        _expBar.fillAmount = exp / maxExp;
    }

    void UpdateShield(float value)
    {
        if (value <= 0)
        {
            _shieldText.text = "";
        }
        else
        {
            _shieldText.text = value.ToString("N0");
        }
    }

    private void OnDisable()
    {
        _inventoryController.nextStage -= SetHealthBar;
        _player.PlayerHealthChange -= UpdateHealthBar;
        _player.PlayerExpChange -= UpdateExpBar;
        _player.PlayerSheildChange -= UpdateShield;
    }
}
