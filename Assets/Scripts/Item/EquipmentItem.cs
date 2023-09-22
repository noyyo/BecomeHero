using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Parts
{
    Weapon,
    Helmet,
    Shoulder,
    Armour,
    Leggings,
    Shoes,
}
public class EquipmentItem : Item
{
    public Parts EquipmentParts;
    public float value;
}
