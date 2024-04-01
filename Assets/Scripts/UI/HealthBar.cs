using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image _healthBar;
    [SerializeField] TextMeshProUGUI _healthText;

    Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
        _player.PlayerHealthChange += UpdateHealthBar;
        _healthText.text = $"{_player.currentStat.health} / {_player.currentStat.maxHealth}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateHealthBar(float maxHealth, float health)
    {
        _healthText.text = $"{health} / {maxHealth}";
        _healthBar.fillAmount = health / maxHealth;
    }
}
