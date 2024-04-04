using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

[System.Serializable]
public class UpgradeData : GameTask
{
    [SerializeField] protected UpgradeType _upgradeType;
    [SerializeField] protected short _maxGrade;
    [SerializeField] protected float _reward;
    [SerializeField] protected short _currentGrade;

    #region Costs
    [SerializeField] short _cost_1;
    [SerializeField] short _cost_2;
    [SerializeField] short _cost_3;
    [SerializeField] short _cost_4;
    [SerializeField] short _cost_5;
    #endregion
    #region Modifiers
    [SerializeField] float _modifier_1;
    [SerializeField] float _modifier_2;
    [SerializeField] float _modifier_3;
    [SerializeField] float _modifier_4;
    [SerializeField] float _modifier_5;
    #endregion
    protected CostModifier[] _costModifiers;

    public UpgradeType UpgradeType => _upgradeType;
    public short MaxGrade => _maxGrade;
    public short CurrentGrade => _currentGrade;
    // reward는 현재 상태 받아와서 반환하게.
    public CostModifier[] CostModifiers => _costModifiers;

    public UpgradeData() { }

    public UpgradeData(int id)
    {
        //ID 로 데이터 가져오고
        DataBase.PlayerUpgrade.Get(id);

        //저장된 데이터에서 currentGrade 가져와야함.
        _currentGrade = (short)PlayerPrefs.GetInt($"Upgrade_{_type}");

        _costModifiers = new CostModifier[_maxGrade];
        SetCostModifiers(0, _cost_1, _modifier_1);
        SetCostModifiers(1, _cost_2, _modifier_2);
        SetCostModifiers(2, _cost_3, _modifier_3);
        SetCostModifiers(3, _cost_4, _modifier_4);
        SetCostModifiers(4, _cost_5, _modifier_5);
    }

    void SetCostModifiers(short index, short cost, float modifier)
    {
        if (index <= _costModifiers.Length)
        {
            _costModifiers[index] = new CostModifier(cost, modifier);
        }
    }
}

public class CostModifier
{
    short _cost;
    float _modifier;

    public CostModifier(short cost, float modifier)
    {
        _cost = cost;
        _modifier = modifier;
    }
}
