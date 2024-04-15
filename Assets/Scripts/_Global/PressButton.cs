using UnityEngine;
using UnityEngine.EventSystems;

public class PressButton : MonoBehaviour, IPointerDownHandler
{
    public string soundTag;
    public void OnPointerDown(PointerEventData eventData)
    {
        SoundManager.Instance.PlayAudioClip(soundTag);
    }
}
