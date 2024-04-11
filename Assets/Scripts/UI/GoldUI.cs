using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;

    Player _player;

    void Start()
    {
        _player = GameManager.Instance.Player;
        InventoryController.Instance.nextStage += UpdateGold;
        _player.PlayerGoldChange += UpdateGold;
        UpdateGold();
    }

    void UpdateGold()
    {
        _text.text = _player.Data.Gold.ToString();
    }
}
