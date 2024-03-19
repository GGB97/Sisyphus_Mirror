using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEquipable
{
    void OnEquip();
    void OnUnequip();
}

public class EquipmentManager : SingletoneBase<EquipmentManager>
{
    public Transform Player { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
    }

    public void OnEquip()
    {
        if (Player != null)
        {
            // TODO : Player 스탯 반영하기
        }
    }
}
