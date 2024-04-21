using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : UI_Base
{
    UIManager _uiManager;
    private void Awake()
    {
        _uiManager = UIManager.Instance;
    }
    private void OnEnable()
    {
        _uiManager.AddActiveUI(gameObject);
    }
    private void OnDisable()
    {
        _uiManager.RemoveActiveUI(gameObject);
    }
}
