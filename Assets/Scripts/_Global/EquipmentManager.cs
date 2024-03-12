using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : SingletoneBase<EquipmentManager>
{
    public Transform Player { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEquip()
    {
        if (Player != null)
        {

        }
    }
}
