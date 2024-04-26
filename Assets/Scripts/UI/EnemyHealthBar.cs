using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] Enemy enemy;

    [SerializeField] Image healthBar;
    [SerializeField] TMP_Text hpText;

    private void Awake()
    {
        enemy.changeHealth += UpdateHealthBar;
    }

    void UpdateHealthBar(float currentHp, float maxHp)
    {
        healthBar.fillAmount = currentHp / maxHp;
        hpText.text = $"{((currentHp / maxHp) * 100f).ToString("N1")} %";
    }
}
