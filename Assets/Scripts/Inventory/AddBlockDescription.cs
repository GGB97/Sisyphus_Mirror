using TMPro;
using UnityEngine;

public class AddBlockDescription : MonoBehaviour
{
    public TextMeshProUGUI description;

    public void SetText(int addCount)
    {
        description.text = string.Format($"레벨 업! \n확장할 {Utilities.SetStringGreen(addCount)}칸을 누르세요");
    }
    public void Active(bool isActive)
    {
        InventoryController.Instance.levelUpBG.SetActive(isActive);
        gameObject.SetActive(isActive);
    }
}
