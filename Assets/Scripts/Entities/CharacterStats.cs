using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatChangeType
{
    Add,
    Multiple,
    Override,

}
public enum StatTypes : int
{
    MaxHealth,
    MoveSpeed,
    // AttackSO
    AttackSize,
    Delay,
    Power,
    Level,
    Exp,
}


[Serializable]
public class CharacterStats
{
    [Range(5, 100000)] public int MaxHealth;
    [Range(0.1f, 100)] public float MoveSpeed;
    public AttackSO AttackInfo;

    // UnityObject will be null
    public CharacterStats Clone()
    {
        CharacterStats stats = new CharacterStats();
        stats.MaxHealth = MaxHealth;
        stats.MoveSpeed = MoveSpeed;
        stats.AttackInfo = null;
        return stats;
    }
    public static string StatToString(StatTypes statType)
    {
        switch (statType)
        {
            case StatTypes.MaxHealth:
                return "최대 체력";
            case StatTypes.MoveSpeed:
                return "이동속도";
            case StatTypes.AttackSize:
                return "공격범위";
            case StatTypes.Delay:
                return "공격속도";
            case StatTypes.Power:
                return "공격력";
            default:
                return "";
        }
    }
}
