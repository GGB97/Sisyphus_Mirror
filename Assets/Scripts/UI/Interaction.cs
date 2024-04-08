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
    public UI_Base OpenUI;               // 상호작용시 나올 UI
    [SerializeField] private bool onInteract = false;        // 상호작용할 거리에 있는지 확인

    UnityEngine.InputSystem.PlayerInput input;

    private void Awake()
    {
        input = GetComponent<UnityEngine.InputSystem.PlayerInput>();
        input.enabled = false;
    }

    public void OnInteraction()
    {
        //Debug.Log($"{gameObject.name} is Try Interaction");
        if (onInteract == true)
        {
            OpenUI.gameObject.SetActive(true);
        }
        else return;
    }

    public void OnExit()
    {
        OpenUI.CloseUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (LayerData.Player == (1 << other.gameObject.layer | LayerData.Player))
        {
            InteractionInfo.SetActive(true);
            onInteract = true;
            input.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (LayerData.Player == (1 << other.gameObject.layer | LayerData.Player))
        {
            InteractionInfo.SetActive(false);
            onInteract = false;
            input.enabled = false;
            OpenUI.CloseUI();
        }
    }
}
