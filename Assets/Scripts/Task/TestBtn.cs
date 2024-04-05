using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
