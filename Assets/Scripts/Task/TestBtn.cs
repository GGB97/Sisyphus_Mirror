using UnityEngine;

public class TestBtn : MonoBehaviour
{
    public void Open()
    {
        UpgradeManager.Instance.OnpenUI();
    }

    public void Apply()
    {
        GameManager.Instance.Player.SetUpgradeModifier();
    }
}
