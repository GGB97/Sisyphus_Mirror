using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletoneBase<PlayerManager>
{

    public List<GameObject> players = new List<GameObject>();
    private GameObject currentPlayer;

    [SerializeField] CinemachineVirtualCamera vcam;
    private void Awake()
    {
      
    }

    private void Start()
    {
        currentPlayer = Instantiate(players[0]);

        vcam.Follow = currentPlayer.transform;
        vcam.LookAt = currentPlayer.transform;
    }

    public void ChangePlayer(int index)
    {
        Destroy(currentPlayer);
        currentPlayer = Instantiate(players[index]);

        vcam.Follow = currentPlayer.transform;
        vcam.LookAt = currentPlayer.transform;
    }
}
