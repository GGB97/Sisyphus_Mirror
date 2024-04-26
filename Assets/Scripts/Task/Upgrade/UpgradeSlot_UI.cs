using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSlot_UI : MonoBehaviour
{
    public int id;
    [SerializeField] TMP_Text _title;
    [SerializeField] TMP_Text _desc;

    [SerializeField] TMP_Text _cost;
    [SerializeField] Button _confirm;

    [SerializeField] GameObject _currentGrade;
    [SerializeField] Image[] _currentGradeImages;

    private void Start()
    {
        _confirm.onClick.AddListener(() =>
        {
            bool check = UpgradeManager.Instance.Upgrade(this);
        });
    }

    public void UpdateUI(UpgradeData data)
    {
        _title.text = data.Name;
        _desc.text = data.Desc.Replace("#N", data.NextReward());
        _cost.text = data.NextCount().ToString();

        for (int i = 0; i < _currentGradeImages.Length; i++)
        {
            if (i < data.CurrentGrade)
                _currentGradeImages[i].color = Color.yellow;
            else
                _currentGradeImages[i].color = new Color(56, 56, 56);
        }

        if (data.CurrentGrade == data.MaxGrade)
        {
            _confirm.gameObject.SetActive(false);
        }
        else
        {
            _confirm.gameObject.SetActive(true);
        }
    }
}
