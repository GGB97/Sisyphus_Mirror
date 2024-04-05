using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class InventoryStats : MonoBehaviour
{
    private static InventoryStats instance;
    public static InventoryStats Instance {  get { return instance; } }
    private CharacterBase characterBase;
    [SerializeField]
    private TextMeshProUGUI statsText;
    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this);
    }
    void Start()
    {
        characterBase = GameManager.Instance.Player.Data;
    }

    public void UpdateStatsPanel()
    {
        StringBuilder sb = new StringBuilder(300);
        Utilities.AddText(sb, "체력", characterBase.maxHealth);
        Utilities.AddText(sb, "방어력", characterBase.def);
        Utilities.AddText(sb, "물리 공격력", characterBase.physicalAtk);
        Utilities.AddText(sb, "마법 공격력", characterBase.magicAtk);
        Utilities.AddText(sb, "공격 범위", characterBase.attackRange,true,false);
        Utilities.AddText(sb, "공격 속도", characterBase.attackSpeed, true, false);
        Utilities.AddText(sb, "이동 속도", characterBase.moveSpeed, true, false);
        Utilities.AddText(sb, "치명타 확률", characterBase.critRate, true, false);
        Utilities.AddText(sb, "치명타 데미지", characterBase.critDamage);
        Utilities.AddText(sb, "피해 흡혈", characterBase.lifeSteal, true, false);
        statsText.text = sb.ToString();
        sb = null;
    }
}
