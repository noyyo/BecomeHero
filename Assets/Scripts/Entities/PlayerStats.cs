using System;
using UnityEngine;

[Serializable]
public class PlayerStats
{
    public string Name;
    [Range(1, 100)] public int Level;
    [Range(0, 100000)] public int Exp;
}