using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] protected List<CostModifier> _costModifiers;

    public UpgradeType UpgradeType => _upgradeType;
    public short MaxGrade => _maxGrade;
    public List<CostModifier> CostModifiers => _costModifiers;
    public override int Count
    {
        get
        {
            Check();
            int index = Index();

            return _costModifiers[index].Cost;
        }
    }
    public float Reward
    {
        get
        {
            Check();
            int index = Index();

            return _costModifiers[index].Modifier;
        }
    }

    public short CurrentGrade
    {
        get
        {
            _currentGrade = (short)PlayerPrefs.GetInt($"Upgrade_{UpgradeType}"); // 데이터 저장 구현시 변경

            return _currentGrade;
        }
    }

    public void Init()
    {
        _currentGrade = (short)PlayerPrefs.GetInt($"Upgrade_{UpgradeType}"); // 데이터 저장 구현시 변경

        _costModifiers = new(MaxGrade);
        SetCostModifiers(0, 0); // 0단계
        SetCostModifiers(_cost_1, _modifier_1);
        SetCostModifiers(_cost_2, _modifier_2);
        SetCostModifiers(_cost_3, _modifier_3);
        SetCostModifiers(_cost_4, _modifier_4);
        SetCostModifiers(_cost_5, _modifier_5);
    }

    void SetCostModifiers(short cost, float modifier)
    {
        if (_costModifiers.Count < MaxGrade + 1)
        {
            _costModifiers.Add(new(cost, modifier));
        }
    }

    public void Reset()
    {
        PlayerPrefs.SetInt($"Upgrade_{UpgradeType}", 0);

        int n = PlayerPrefs.GetInt($"Upgrade_{UpgradeType}");

        Debug.Log($"pp reset : {n}");
    }

    public string NextReward()
    {
        Check();
        int index = Index(1);

        return CostModifiers[index].Modifier.ToString();
    }

    public string NextCount()
    {
        Check();
        int index = Index(1);

        return CostModifiers[index].Cost.ToString();
    }

    int Index(int modi = 0)
    {
        int index = CurrentGrade + modi;
        if (index >= _costModifiers.Count)
            index = _costModifiers.Count - 1;

        return index;
    }

    void Check() // 초기화 안되있으면 초기화시킴
    {
        if (_costModifiers == null || _costModifiers.Count == 0)
            Init();
    }
}

[System.Serializable]
public class CostModifier
{
    [SerializeField] short _cost;
    [SerializeField] float _modifier;

    public short Cost => _cost;
    public float Modifier => _modifier;

    public CostModifier(short cost, float modifier)
    {
        _cost = cost;
        _modifier = modifier;
    }
}
