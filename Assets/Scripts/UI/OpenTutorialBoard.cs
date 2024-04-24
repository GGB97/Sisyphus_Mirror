using UnityEngine;
using UnityEngine.UI;

public class OpenTutorialBoard : MonoBehaviour
{
    [SerializeField] Button _openTutorialButton;

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
    }

    private void OnDisable()
    {
        _openTutorialButton.onClick.RemoveListener(OnClickOpenTutorialBoard);
    }
}
