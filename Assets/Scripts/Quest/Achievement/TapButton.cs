using UnityEngine;
using UnityEngine.Events;

public class TapButton : MonoBehaviour
{
    public UnityEvent onTabSelected;//눌렀을 때 실행할 이벤트
    public UnityEvent onTabUnselected;//해제했을 때 실행항 이벤트
    // Start is called before the first frame update

    public void Select()
    {
        if (onTabSelected != null)
            onTabSelected.Invoke();
    }

    public void Unselect()
    {
        if (onTabUnselected != null)
            onTabUnselected.Invoke();
    }
    public void OnSelectTab(TapButton button)
    {
        TapController.Instance.SelectedButton(button);
    }
}
