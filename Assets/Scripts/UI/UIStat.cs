using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIStat : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _statTypeText;
    [SerializeField] private TextMeshProUGUI _valueText;
    public StatTypes StatType;
    private float _value;
    public float Value
    {
        get { return _value; }
        set
        {
            _value = value;
            switch (StatType)
            {
                case StatTypes.MaxHealth:
                    _valueText.text = Mathf.RoundToInt(_value).ToString();
                    break;
                case StatTypes.MoveSpeed:
                case StatTypes.AttackSize:
                case StatTypes.Delay:
                case StatTypes.Power:
                    _valueText.text = _value.ToString("F2");
                    break;
            }
        }
    }
    private void Awake()
    {
        _statTypeText.text = CharacterStats.StatToString(StatType);
    }
}
