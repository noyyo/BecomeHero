using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
using UnityEngine;

public class CharacterStatHandler : MonoBehaviour
{
    [SerializeField] protected CharacterStats BaseStats;
    public CharacterStats CurrentCharacterStats { get; protected set; }
    private List<StatModifier> _statsModifiers = new List<StatModifier>();

    private const int MinMaxHealth = 5;
    private const float MinMoveSpeed = 0.1f;
    private const float MinAttackDelay = 0.1f;
    private const float MinAttackPower = 1f;
    private const float MinAttackSize = 1f;

    private void Awake()
    {
        InitializeCharacterStats();
    }
    private void Start()
    {
        UpdateCharacterStats();
    }
    public void AddStatModifier(StatModifier statModifier)
    {
        _statsModifiers.Add(statModifier);
        UpdateCharacterStats();
    }
    public void RemoveStatModifier(StatModifier statModifier)
    {
        _statsModifiers.Remove(statModifier);
        UpdateCharacterStats();
    }
    protected void InitializeCharacterStats()
    {
        AttackSO attackInfo = null;
        if (BaseStats.AttackInfo != null)
        {
            attackInfo = Instantiate(BaseStats.AttackInfo);
        }

        CurrentCharacterStats = BaseStats.Clone();
        CurrentCharacterStats.AttackInfo = attackInfo;
    }
    protected void UpdateCharacterStats()
    {
        foreach (StatModifier modifier in _statsModifiers.OrderBy(modifier => modifier.StatChangeType))
        {
            if (modifier.StatChangeType == StatChangeType.Override)
            {
                UpdateStats(modifier.StatType, modifier.Value, (o1, o2) => o2);
            }
            else if (modifier.StatChangeType == StatChangeType.Add)
            {
                UpdateStats(modifier.StatType, modifier.Value, (o1, o2) => o1 + o2);
            }
            else if (modifier.StatChangeType == StatChangeType.Multiple)
            {
                UpdateStats(modifier.StatType, modifier.Value, (o1, o2) => o1 * o2);
            }
        }

        LimitAllStats();
    }
    protected virtual void UpdateStats(StatTypes statType, float value, Func<float, float, float> operation)
    {
        switch (statType)
        {
            case StatTypes.MaxHealth:
                CurrentCharacterStats.MaxHealth = (int)operation(CurrentCharacterStats.MaxHealth, value);
                break;
            case StatTypes.MoveSpeed:
                CurrentCharacterStats.MoveSpeed = operation(CurrentCharacterStats.MoveSpeed, value);
                break;
        }
        if (CurrentCharacterStats.AttackInfo == null)
            return;
        switch (statType)
        {
            case StatTypes.AttackSize:
                CurrentCharacterStats.AttackInfo.AttackSize = operation(CurrentCharacterStats.AttackInfo.AttackSize, value);
                break;
            case StatTypes.Delay:
                CurrentCharacterStats.AttackInfo.Delay = operation(CurrentCharacterStats.AttackInfo.Delay, value);
                break;
            case StatTypes.Power:
                CurrentCharacterStats.AttackInfo.Power = operation(CurrentCharacterStats.AttackInfo.Power, value);
                break;
        }
    }

    protected virtual void LimitAllStats()
    {
        if (CurrentCharacterStats == null)
        {
            return;
        }

        CurrentCharacterStats.MaxHealth = Mathf.Max(CurrentCharacterStats.MaxHealth, MinMaxHealth);
        CurrentCharacterStats.MoveSpeed = Mathf.Max(CurrentCharacterStats.MoveSpeed, MinMoveSpeed);

        if (CurrentCharacterStats.AttackInfo == null)
        {
            return;
        }

        CurrentCharacterStats.AttackInfo.AttackSize = Mathf.Max(CurrentCharacterStats.AttackInfo.AttackSize, MinAttackSize);
        CurrentCharacterStats.AttackInfo.Delay = Mathf.Max(CurrentCharacterStats.AttackInfo.Delay, MinAttackDelay);
        CurrentCharacterStats.AttackInfo.Power = Mathf.Max(CurrentCharacterStats.AttackInfo.Power, MinAttackPower);
    }
    public virtual float GetCurrentStatValue(StatTypes statType)
    {
        switch (statType)
        {
            case StatTypes.MaxHealth:
                return CurrentCharacterStats.MaxHealth;
            case StatTypes.MoveSpeed:
                return CurrentCharacterStats.MoveSpeed;
        }

        if (CurrentCharacterStats == null)
            return 0;

        switch (statType)
        {
            case StatTypes.AttackSize:
                return CurrentCharacterStats.AttackInfo.AttackSize;
            case StatTypes.Delay:
                return CurrentCharacterStats.AttackInfo.Delay;
            case StatTypes.Power:
                return CurrentCharacterStats.AttackInfo.Power;
        }
        return 0;
    }
}