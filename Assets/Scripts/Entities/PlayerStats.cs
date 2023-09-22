using System;
using UnityEngine;

[Serializable]
public class PlayerStats : CharacterStats
{
    [SerializeField] public string name;
    [SerializeField] [Range(1, 100)] public int Level;
    [SerializeField] [Range(0, 100000)] public int Exp;
}