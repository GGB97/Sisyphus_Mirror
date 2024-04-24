using UnityEngine;
using UnityEngine.UI;

public class OpenTutorialBoard : MonoBehaviour
{
    [SerializeField] Button _openTutorialButton;
    [SerializeField] GameObject _menu;

    private void Start()
    {
        _openTutorialButton.GetComponent<Button>();
    }

    private void OnEnable()
    {
        _openTutorialButton.onClick.AddListener(OnClickOpenTutorialBoard);
    }

    void OnClickOpenTutorialBoard()
    {
        TutorialManager.Instance.GetTutorialBoard();
        if(_menu != null && _menu.activeSelf == true)
        {
            _menu.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }

    private void OnDisable()
    {
        _openTutorialButton.onClick.RemoveListener(OnClickOpenTutorialBoard);
    }
}
