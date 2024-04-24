using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Image coolDown;
    [SerializeField] TMP_Text coolDonwText;

    StringBuilder sb = new();

    private void Start()
    {
        Skill_Base skill = GameManager.Instance.Player.GetComponent<Skill_Base>();

        icon.sprite = skill.icon;
        coolDown.sprite = skill.icon;
        skill.cooldownUpdate += UpdateUI;

        coolDonwText.text = "";
    }

    void UpdateUI(bool isCooldown, bool isActive, float max, float current)
    {
        if (isCooldown == false)
        {
            coolDonwText.text = "";
            coolDown.fillAmount = 0;
            return;
        }

        if (isActive)
            coolDown.color = new(1, 0, 0, 0.5f);
        else
            coolDown.color = new(0, 0, 0, 0.5f);

        coolDown.fillAmount = current / max;


        //if(current < 0.01f && isActive == false)
        //{
        //    coolDonwText.text = "";
        //    return;
        //}
        coolDonwText.text = current.ToString("N1");
    }
}
