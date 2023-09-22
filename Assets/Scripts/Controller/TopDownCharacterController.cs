using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCharacterController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action OnAttackEvent;

    private CharacterStatHandler _statHandler;
    private AttackSO _attackInfo;

    protected bool IsAttacking;
    private float _timeSinceLastAttack;
    private void Awake()
    {
        _statHandler = GetComponent<CharacterStatHandler>();
        _attackInfo = _statHandler.CurrentCharacterStats.AttackInfo;
    }

    private void Update()
    {
        if (_attackInfo != null && IsAttacking && _timeSinceLastAttack > _attackInfo.Delay)
        {
            CallAttackEvent();
        }
    }

    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent.Invoke(direction);
    }

    public void CallLookEvent(Vector2 direction)
    {
        OnLookEvent.Invoke(direction);
    }

    public void CallAttackEvent()
    {
        OnAttackEvent.Invoke();
    }
}
