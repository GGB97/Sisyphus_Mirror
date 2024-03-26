using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDescription : MonoBehaviour
{
    public InventoryItem currentItem;
    public RectTransform rectTransform;
    public Vector3 direction = new Vector3(500f, 0,0);
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void SetTransform()
    {
        RectTransform newtransform = currentItem.gameObject.GetComponent<RectTransform>();
        rectTransform.position = newtransform.position + direction;
    }
}
