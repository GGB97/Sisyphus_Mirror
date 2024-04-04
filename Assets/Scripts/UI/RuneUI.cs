using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RuneUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;

    Player _player;

    void Start()
    {
        _player = GameManager.Instance.Player;
        _player.PlayerRuneChange += UpdateRune;
        UpdateRune();
    }

    void UpdateRune()
    {
        _text.text = _player.rune.ToString();
    }
}
