using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletoneBase<PlayerManager>
{

    public List<GameObject> players = new List<GameObject>();
    private GameObject currentPlayer;

    private void Awake()
    {
      
    }

    private void Start()
    {
        currentPlayer = Instantiate(players[0]);
    }

    public void ChangePlayer(int index)
    {
        Destroy(currentPlayer);
        currentPlayer = Instantiate(players[index]);
    }
}
