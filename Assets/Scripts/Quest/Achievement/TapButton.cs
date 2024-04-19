using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TapButton : MonoBehaviour
{
    public UnityEvent onTabSelected;
    public UnityEvent onTabUnselected;
    // Start is called before the first frame update

    public void Select()
    {
        if(onTabSelected != null)
            onTabSelected.Invoke();
    }

    public void Unselect()
    {
        if(onTabUnselected != null)
            onTabUnselected.Invoke();
    }
    public void OnSelectTab(TapButton button)
    {
        TapController.Instance.SelectedButton(button);
    }
}
