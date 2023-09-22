using System;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

public class PlayerStatHandler : CharacterStatHandler
{
    [SerializeField] private PlayerStats _playerStats;
    private const int MaxNameLength = 6;
    private const int MinLevel = 1;
    private const int MaxLevel = 100;
    private const int MinExp = 0;
    private const int MaxExp = 100000;
    public int ExpForLevelUp
    {
        get
        {
            return Mathf.RoundToInt(Mathf.Pow(100, _playerStats.Level));
        }
    }

    private void Awake()
    {
        InitializeCharacterStats();
    }
    private void Start()
    {
        UpdateCharacterStats();
    }
    protected override void UpdateStats(StatTypes statType, float value, Func<float, float, float> operation)
    {
        base.UpdateStats(statType, value, operation);

        switch (statType)
        {
            case StatTypes.Level:
                _playerStats.Level = Mathf.RoundToInt(operation(_playerStats.Level, value));
                break;
            case StatTypes.Exp:
                _playerStats.Exp = Mathf.RoundToInt(operation(_playerStats.Exp, value));
                while (_playerStats.Exp > ExpForLevelUp)
                {
                    // watch out order
                    _playerStats.Exp -= ExpForLevelUp;
                    _playerStats.Level++;
                }
                break;
        }
    }

    protected override void LimitAllStats()
    {
        base.LimitAllStats();
        _playerStats.Level = Mathf.Clamp(_playerStats.Level, MinLevel, MaxLevel);
        _playerStats.Exp = Mathf.Clamp(_playerStats.Level, MinExp, MaxExp);
    }

    public void SetName(string name)
    {
        if (name.Length > MaxNameLength)
            _playerStats.Name = name.Substring(0, MaxNameLength);
    }
    public string GetName()
    {
        return _playerStats.Name;
    }
    public PlayerStats GetPlayerStats()
    {
        return _playerStats;
    }

    public override float GetCurrentStatValue(StatTypes statType)
    {
        switch (statType)
        {
            case StatTypes.Level:
                return _playerStats.Level;
            case StatTypes.Exp:
                return _playerStats.Exp;
        }

        return base.GetCurrentStatValue(statType);
    }
}