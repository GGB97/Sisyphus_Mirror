using TMPro;
using UnityEngine;

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
