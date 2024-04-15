using System.Text;
using TMPro;
using UnityEngine;

public class RuneStonePlayerStat : MonoBehaviour
{
    private Status playerStats;
    [SerializeField] private TextMeshProUGUI statsText;

    void OnEnable()
    {
        playerStats = GameManager.Instance.Player.currentStat;
        UpdateStatsPanel();
    }

    public void UpdateStatsPanel()
    {
        playerStats = GameManager.Instance.Player.currentStat;

        StringBuilder sb = new StringBuilder(300);
        Utilities.AddText(sb, "체력", playerStats.maxHealth);
        Utilities.AddText(sb, "방어력", playerStats.def);
        Utilities.AddText(sb, "물리 공격력", playerStats.physicalAtk);
        Utilities.AddText(sb, "마법 공격력", playerStats.magicAtk);
        Utilities.AddText(sb, "공격 범위", playerStats.attackRange, true, false);
        Utilities.AddText(sb, "공격 속도", playerStats.attackSpeed, true, false);
        Utilities.AddText(sb, "이동 속도", playerStats.moveSpeed, true, false);
        Utilities.AddText(sb, "치명타 확률", playerStats.critRate, true, false);
        Utilities.AddText(sb, "치명타 데미지", playerStats.critDamage);
        Utilities.AddText(sb, "피해 흡혈", playerStats.lifeSteal, true, false);
        Utilities.AddText(sb, "무게", playerStats.weight, false, true);
        statsText.text = sb.ToString();
        sb = null;
    }
}
