using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDrag : MonoBehaviour,IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    private InventoryController inventoryController;
    private Image image;
    private ItemDescription itemDesription;

    public float hoverTime = 0.2f;
    public bool isHovering;
    public bool isPressed = false;
    private Coroutine displayCoroutine;

    private void Awake()
    {
        inventoryController = InventoryController.Instance;
        image = GetComponent<Image>();
    }
    public void OnPointerDown(PointerEventData eventData) //마우스를 누른 순간 실행
    {
        if (isPressed == true)
            return;

        if (eventData.button == PointerEventData.InputButton.Left)//마우스 왼쪽 클릭일 때만
        {
            Debug.Log("Click Down");
            image.raycastTarget = false;
            inventoryController.startPosition = transform.position; ;//시작 위치 지정
            inventoryController.LeftMouseButtonPress();
        }
        else if (eventData.button == PointerEventData.InputButton.Right && inventoryController.SelectedItemGrid == inventoryController.playerInventoryGrid)//오른쪽 눌렀을 때
        {
            isPressed = true;
            if(itemDesription != null)
                itemDesription.RightClick();
            //if(inventoryController.SelectedItemGrid == inventoryController.playerInventoryGrid)
            //inventoryController.SellItemButton(); 아이템 삭제
        }
    }

    public void OnPointerUp(PointerEventData eventData)//손을 뗐을 때 실행
    {
        if (eventData.button == PointerEventData.InputButton.Left)//마우스 왼쪽 클릭일 때만
        {
            image.raycastTarget = true;
            Debug.Log("Click Up");
            inventoryController.LeftMouseButtonPut();
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (inventoryController.selectedItem != null)
        {
            return;
        }

        if (itemDesription == null)
        {
            itemDesription = inventoryController.itemDescriptionUI.GetComponent<ItemDescription>();
            if (itemDesription == null)
                return;
        }
        isHovering = true;
        displayCoroutine = StartCoroutine(WaitSecondsOnUI());//아이템 이미지 위에서 hover시간 만큼 기다리기
        Debug.Log("Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ExitUI();
        Debug.Log("Exit");
    }


    private void OnDestroy()//삭제될 때 실행
    {
        ExitUI();
    }
    IEnumerator WaitSecondsOnUI()//시간 기다리고 UI 띄우기
    {
        yield return new WaitForSeconds(hoverTime); // 일정 시간 동안 대기
        if (isHovering) // 만약 마우스가 아직 아이템 위에 있는 경우
        {
            itemDesription.currentItem = GetComponent<InventoryItem>();
            itemDesription.gameObject.SetActive(true);
            itemDesription.SetTransform();
            //itemDesription.transform.SetParent(this.transform);
            itemDesription.transform.SetAsLastSibling();
            Debug.Log("호출");
        }
    }
    public  void ExitUI()
    {
        if (isPressed == false)
        {
            if (itemDesription != null)
            {
                itemDesription.SetCurrentItemNull();
                itemDesription.gameObject.SetActive(false);
            }
            isHovering = false; // 마우스가 아이템 위에 없음을 표시
            if (displayCoroutine != null)
            {
                StopCoroutine(displayCoroutine); // 딜레이 후 UI 표시 코루틴 중지
            }
        }
    }
}
