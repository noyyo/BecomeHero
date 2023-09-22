using System;
using UnityEngine;

public class PlayerStatsHandler : CharacterStatHandler
{
    
    private const int MinLevel = 1;
    private const int MaxLevel = 100;
    private const int MinExp = 0;
    private const int MaxExp = 100000;

    private void Awake()
    {
        InitializeCharacterStats();
    }
    protected override void InitializeCharacterStats()
    {
        PlayerStats playerStats = new PlayerStats();

        AttackSO attackInfo = null;
        if (baseStats == null)
        {
            Debug.Log("오");
        }
        if (baseStats.AttackInfo != null)
        {
            attackInfo = Instantiate(baseStats.AttackInfo);
        }

        CurrentCharacterStats = baseStats.Clone();
        CurrentCharacterStats.AttackInfo = attackInfo;
    }
    protected override void UpdateStats(StatType statType, float value, Func<float, float, float> operation)
    {
        base.UpdateStats(statType, value, operation);

        if (!(CurrentCharacterStats is PlayerStats))
        {
            return;
        }

        PlayerStats playerStats = (PlayerStats)CurrentCharacterStats;

        switch (statType)
        {
            case StatType.Level:
                playerStats.Level = Mathf.RoundToInt(operation(playerStats.Level, value));
                break;
            case StatType.Exp:
                playerStats.Exp = Mathf.RoundToInt(operation(playerStats.Exp, value));
                break;
        }
    }

    protected override void LimitAllStats()
    {
        base.LimitAllStats();

        if (!(CurrentCharacterStats is PlayerStats))
        {
            return;
        }

        PlayerStats playerStats = (PlayerStats)CurrentCharacterStats;
        playerStats.Level = Mathf.Clamp(playerStats.Level, MinLevel, MaxLevel);
        playerStats.Exp = Mathf.Clamp(playerStats.Level, MinExp, MaxExp);

    }
}