using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_AttackSO_Data", menuName = "ScriptableObject/Attacks/AttackSO", order = 1)]
public class AttackSO : ScriptableObject
{
    [Header("Attack Info")]
    public float AttackSize;
    public float Delay;
    public float Power;
}
