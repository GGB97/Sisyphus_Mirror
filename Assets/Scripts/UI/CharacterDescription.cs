using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int characterID;
    public GameObject descriptionUI;
    
    [SerializeField] private TextMeshProUGUI description;

 

    public void OpenUI()
    {
        descriptionUI.SetActive(true);
        
        Description();
        SetTransform();
    }

    public void CloseUI()
    {
        descriptionUI.SetActive(false);
    }

    public void Description()
    {
        PlayerBaseData playerData = DataBase.Player.Get(characterID);
        
        StringBuilder sb = new StringBuilder(200);
        sb.Append($"\n{playerData.description}\n");
        sb.Append("\n");
        Utilities.AddText(sb, "최대 체력", playerData.maxHealth);
        Utilities.AddText(sb, "방어력", playerData.def);
        Utilities.AddText(sb, "물리 공격력", playerData.physicalAtk);
        Utilities.AddText(sb, "마법 공격력", playerData.magicAtk);
        Utilities.AddText(sb, "공격 속도", playerData.attackSpeed);
        Utilities.AddText(sb, "이동 속도", playerData.moveSpeed);
        Utilities.AddText(sb, "치명타 확률", playerData.critRate);
        Utilities.AddText(sb, "치명타 데미지", playerData.critDamage);
        Utilities.AddText(sb, "피해 흡혈", playerData.lifeSteal);
        description.text = sb.ToString();
    }

    public void SetTransform()
    {
        RectTransform transform = gameObject.GetComponent<RectTransform>();
        
        // 
        float posX = (transform.rect.width + 500) / 2;    
        
        Vector2 newPos = transform.position + new Vector3(posX, -100, 0);
        descriptionUI.transform.position = newPos;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OpenUI();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CloseUI();
    }
}
