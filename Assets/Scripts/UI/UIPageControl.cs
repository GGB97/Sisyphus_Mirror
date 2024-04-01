using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPageControl : MonoBehaviour
{
    [SerializeField] private Toggle toggle_Base; // 복사 원본 페이지 인디케이터

    private List<Toggle> list_Toggle = new List<Toggle>(); //페이지 인디케이터를 저장

    private void Awake()
    {
        toggle_Base.gameObject.SetActive(false);  // 복사 원본 페이지 인디케이터는 비활성화시켜 둔다
    }

    public void SetNumberOfPages(int number)
    {
        if (list_Toggle.Count < number)
        {
            // 페이지 인디케이터 수가 지정된 페이지 수보다 적으면
            // 복사 원본 페이지 인디케이터로부터 새로운 페이지 인디케이터를 작성
            for (int i = list_Toggle.Count; i <number; i++)
            {
                Toggle indicator = Instantiate(toggle_Base) as Toggle;
                indicator.gameObject.SetActive(true);
                indicator.transform.SetParent(toggle_Base.transform.parent);
                indicator.transform.localScale = toggle_Base.transform.localScale;
                indicator.isOn = false;
                list_Toggle.Add(indicator);
            }
        }
        else if(list_Toggle.Count > number)
        {
            // 페이지 인디케이터 수가 지정된 페이지 수보다 많으면 삭제
            for (int i = list_Toggle.Count - 1; i >= number; i--)
            {
                Destroy(list_Toggle[i].gameObject);
                list_Toggle.RemoveAt(i);
            }
        }
    }

    // 현재 페이지를 설정하는 메서드
    public void SetCurrentPage(int index)
    {
        if (index >= 0 && index <= list_Toggle.Count - 1)
        {
            // 지정된 페이지에 대응하는 페이지 인디케이터를 ON으로 저장
            // 토글 그룹을 설정해두었으므로 다른 인디케이터는 자동으로 OFF
            list_Toggle[index].isOn = true;
        }
    }
}
