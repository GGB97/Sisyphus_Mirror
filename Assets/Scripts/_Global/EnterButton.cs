using UnityEngine;
using UnityEngine.EventSystems;

public class EnterButton : MonoBehaviour, IPointerEnterHandler
{
    public string soundTag = "ClickButton";
    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.Instance.PlayAudioClip(soundTag);
    }
}
