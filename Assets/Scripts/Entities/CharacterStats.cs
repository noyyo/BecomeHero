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
public enum StatType : int
{
    MaxHealth,
    MoveSpeed,
    // AttackSO
    AttackSize,
    Delay,
    Power,
    Level,
    Exp,
    Name,

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
}
