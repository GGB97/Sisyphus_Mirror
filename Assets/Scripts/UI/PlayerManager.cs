using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public List<GameObject> players = new List<GameObject>();
    private GameObject currentPlayer;
    public Transform transform;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentPlayer = Instantiate(players[0]);
    }

    public void ChangePlayer(int index)
    {
        Destroy(currentPlayer);
        currentPlayer = Instantiate(players[index], transform );
    }
}
