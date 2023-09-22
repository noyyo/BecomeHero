using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterStatHandler : MonoBehaviour
{
    [SerializeField] private CharacterStats baseStats;
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
    protected virtual void InitializeCharacterStats()
    {
        AttackSO attackInfo = null;
        if (baseStats.AttackInfo != null)
        {
            attackInfo = Instantiate(baseStats.AttackInfo);
        }

        CurrentCharacterStats = baseStats.Clone();
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
    protected virtual void UpdateStats(StatType statType, float value, Func<float, float, float> operation)
    {
        switch (statType)
        {
            case StatType.MaxHealth:
                CurrentCharacterStats.MaxHealth = (int)operation(CurrentCharacterStats.MaxHealth, value);
                break;
            case StatType.MoveSpeed:
                CurrentCharacterStats.MoveSpeed = operation(CurrentCharacterStats.MoveSpeed, value);
                break;
        }
        if (CurrentCharacterStats.AttackInfo == null)
            return;
        switch (statType)
        {
            case StatType.AttackSize:
                CurrentCharacterStats.AttackInfo.AttackSize = operation(CurrentCharacterStats.AttackInfo.AttackSize, value);
                break;
            case StatType.Delay:
                CurrentCharacterStats.AttackInfo.Delay = operation(CurrentCharacterStats.AttackInfo.Delay, value);
                break;
            case StatType.Power:
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
}