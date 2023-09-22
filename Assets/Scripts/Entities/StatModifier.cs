using System;
using System.Collections.Generic;

public class StatModifier
{
    public StatType StatType;
    public StatChangeType StatChangeType;
    public float Value;

    public StatModifier(StatType statType, StatChangeType statChangeType, float value)
    {
        StatType = statType;
        StatChangeType = statChangeType;
        Value = value;
    }
}
