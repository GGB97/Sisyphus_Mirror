using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System;

public class Interaction : MonoBehaviour
{
    public GameObject InteractionInfo;
    public GameObject OpenUI;
    private bool onInteract = false;


    public void OnInteraction()
    {
        Debug.Log("eeeee");
        if (onInteract == true)
        {
            OpenUI.SetActive(true);
        }
        else return;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(LayerData.Player == (1 << other.gameObject.layer | LayerData.Player))
        {
            InteractionInfo.SetActive(true);
            onInteract = true;
            Debug.Log(onInteract);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (LayerData.Player == (1 << other.gameObject.layer | LayerData.Player))
        {
            InteractionInfo.SetActive(false);
            onInteract = false;
        }
    }
}
