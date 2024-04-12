using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image _healthBar;
    [SerializeField] TextMeshProUGUI _healthText;

    Player _player;

    void Start()
    {
        _player = GameManager.Instance.Player; //GameObject.FindWithTag("Player").GetComponent<Player>();
        _player.PlayerHealthChange += UpdateHealthBar;
        _healthText.text = $"{_player.currentStat.health} / {_player.currentStat.maxHealth}";
    }

    void UpdateHealthBar(float maxHealth, float health)
    {
        _healthText.text = $"{(int)health} / {maxHealth}";
        _healthBar.fillAmount = health / maxHealth;
    }
}
