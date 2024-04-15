using TMPro;
using UnityEngine;

public class AddBlockDescription : MonoBehaviour
{
    public TextMeshProUGUI description;

    public void SetText(int addCount)
    {
        description.text = string.Format($"레벨 업! {Utilities.ChangeColorWithValue(addCount)}확장할 칸을 누르세요");
    }
    public void Active(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
