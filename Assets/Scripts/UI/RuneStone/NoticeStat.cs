using TMPro;
using UnityEngine;

public class NoticeStat : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _noticeLabel;
    [SerializeField] TextMeshProUGUI _noticeValue;

    public void SetNoticeStatText(string label, string value)
    {
        _noticeLabel.text = label; 
        _noticeValue.text = value;
    }
}
