using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System;

public class Interaction : MonoBehaviour
{
    public GameObject InteractionInfo;      // 상호작용할 오브젝트 정보
    public GameObject OpenUI;               // 상호작용시 나올 UI
    [SerializeField] private bool onInteract = false;        // 상호작용할 거리에 있는지 확인


    public void OnInteraction()
    {
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
