using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapController : MonoBehaviour
{
    private static TapController _instance;
    public static TapController Instance
    {
        get 
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<TapController>();
                if (_instance == null)
                    Debug.LogError("탭 컨트롤이 없습니다.");
            }
            return _instance;
        }
    }
    TapButton tabButton;
    public void SelectedButton(TapButton button)
    {
        if (tabButton != null)//기존 버튼 해제
        {
            tabButton.Unselect();//끔
        }

        tabButton = button; //할당
        tabButton.Select(); //선택
    }

    // Start is called before the first frame update
    private void Start()
    {
        SelectedButton(transform.GetChild(0).GetComponent<TapButton>());//첫 탭칸을 가져옴
    }
}
