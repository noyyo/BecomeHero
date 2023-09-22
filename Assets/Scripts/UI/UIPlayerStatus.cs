using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStatus : UIBase
{
    [SerializeField] private Transform _playerInfo;
    [SerializeField] private Transform _statPanel;
    [SerializeField] private Transform _equipments;
    private TextMeshProUGUI _nameText;
    private TextMeshProUGUI _levelText;
    private Slider _expSlider;
    private GridLayoutGroup _baseStatGridLayout;
    private GridLayoutGroup _attckStatGridLayout;

    private Dictionary<StatTypes, UIStat> _uiStats = new Dictionary<StatTypes, UIStat>();

    private PlayerStatHandler _playerStatHandler;
    
    public override void InitializeUI()
    {
        // !! Watch out index number !!
        _nameText = _playerInfo.GetChild(1).GetComponent<TextMeshProUGUI>();
        _levelText = _playerInfo.GetChild(2).GetComponent<TextMeshProUGUI>();
        _expSlider = _playerInfo.GetChild(4).GetComponent<Slider>();
        _baseStatGridLayout = _statPanel.GetChild(2).GetComponent<GridLayoutGroup>();
        _attckStatGridLayout = _statPanel.GetChild(3).GetComponent<GridLayoutGroup>();

        foreach (UIStat stat in _baseStatGridLayout.GetComponentsInChildren<UIStat>())
        {
            _uiStats.Add(stat.StatType, stat);
        }
        foreach (UIStat stat in _attckStatGridLayout.GetComponentsInChildren<UIStat>())
        {
            _uiStats.Add(stat.StatType, stat);
        }
        _playerStatHandler = GameManager.Instance.Player.GetComponent<PlayerStatHandler>();

    }
    public override void CloseUI()
    {
        gameObject.SetActive(false);
    }

    public override void OpenUI()
    {
        UpdateUI();
        gameObject.SetActive(true);
    }

    public override void UpdateUI()
    {
        UpdateStat();
    }
    private void UpdateStat()
    {
        StatTypes statType;
        for (int i = 0; i < Enum.GetValues(typeof(StatTypes)).Length; i++)
        {
            statType = (StatTypes)i;
            if (_uiStats.ContainsKey(statType))
            {
                _uiStats[statType].Value = _playerStatHandler.GetCurrentStatValue(statType);
            }
        }

        PlayerStats playerStats = _playerStatHandler.GetPlayerStats();
        _nameText.text = playerStats.Name;
        _levelText.text = $"LV : {playerStats.Level}";
        _expSlider.value = (float)playerStats.Exp / _playerStatHandler.ExpForLevelUp;
    }
    
}
