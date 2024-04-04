using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public List<GameObject> players = new List<GameObject>();
    private GameObject currentPlayer;
    
    [SerializeField] CinemachineVirtualCamera vcam;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentPlayer = Instantiate(players[0]);
        //GameManager.Instance.SetPlayer(currentPlayer.GetComponent<Player>());

        vcam.Follow = currentPlayer.transform;
        vcam.LookAt = currentPlayer.transform;
    }

    public void ChangePlayer(int index)
    {
        Destroy(currentPlayer);
        currentPlayer = Instantiate(players[index]);
        //GameManager.Instance.SetPlayer(currentPlayer.GetComponent<Player>());

        vcam.Follow = currentPlayer.transform;
        vcam.LookAt = currentPlayer.transform;
    }
}
