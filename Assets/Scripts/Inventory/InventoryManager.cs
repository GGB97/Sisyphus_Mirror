using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager _inventoryManager;

    //public ItemGrid grid;

    //string gridName = "InventoryGrid";
    public static InventoryManager Instance 
    {  
        get { return _inventoryManager;}
        private set { _inventoryManager = value; } 
    }
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
    }
    public void Init()
    {
    }
}
