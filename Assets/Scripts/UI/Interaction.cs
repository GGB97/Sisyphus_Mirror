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
    public InputActionReference interactAction;

    //private void OnEnable()
    //{
    //    interactAction.action.started += OnInteract;
    //}

    //private void OnDisable()
    //{
    //    interactAction.action.started -= OnInteract;
    //}

    //private void OnInteract(InputAction.CallbackContext context)
    //{
    //    OpenUI.SetActive(true);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if(LayerData.Player == (1 << other.gameObject.layer | LayerData.Player))
        {
            InteractionInfo.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (LayerData.Player == (1 << other.gameObject.layer | LayerData.Player))
        {
            InteractionInfo.SetActive(false);
        }
    }
}
