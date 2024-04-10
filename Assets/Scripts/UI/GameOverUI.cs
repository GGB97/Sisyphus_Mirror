using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recordText;


    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        
        gameObject.SetActive(true);
        UpdateTextUI();
    }

    public void OnClickRetry()
    {
        
        GameManager.Instance.Retry();
    }

    private void UpdateTextUI()
    {
        StringBuilder sb = new StringBuilder(50);
        sb.Append(DungeonManager.Instance.currnetstage);
        sb.Append('\n');
        sb.Append(GameManager.Instance.Player.Data.LV);
        sb.Append("\n");
        sb.Append(GameManager.Instance.killenemys);
        sb.Append("\n");
        sb.Append(GameManager.Instance.totalGold);
        recordText.text = sb.ToString();
        sb = null;
    }
}
