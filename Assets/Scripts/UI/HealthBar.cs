using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image _healthBar;
    [SerializeField] TextMeshProUGUI _healthText;
    [SerializeField] Image _expBar;
    [SerializeField] TextMeshProUGUI _shieldText;

    Player _player;

    void Start()
    {
        _player = GameManager.Instance.Player; //GameObject.FindWithTag("Player").GetComponent<Player>();
        _player.PlayerHealthChange += UpdateHealthBar;
        _healthText.text = $"{_player.currentStat.health} / {_player.currentStat.maxHealth}";
        _player.PlayerExpChange += UpdateExpBar;

        _player.PlayerSheildChange += UpdateShield;
    }

    void UpdateHealthBar(float maxHealth, float health)
    {
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
}
