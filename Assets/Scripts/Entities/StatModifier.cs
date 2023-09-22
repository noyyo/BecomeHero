using System;
using System.Collections.Generic;

public class StatModifier
{
    public StatTypes StatType;
    public StatChangeType StatChangeType;
    public float Value;

    public StatModifier(StatTypes statType, StatChangeType statChangeType, float value)
    {
        StatType = statType;
        StatChangeType = statChangeType;
        Value = value;
    }
}
