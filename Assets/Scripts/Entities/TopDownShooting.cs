using System;
using UnityEngine;

public class TopDownShooting : MonoBehaviour
{
    private TopDownCharacterController _controller;
    private Vector2 _aimDirection = Vector2.right;

    private void Awake()
    {
        _controller = GetComponent<TopDownCharacterController>();
    }

    private void Start()
    {
        _controller.OnLookEvent += SetAimDirection;
        _controller.OnAttackEvent += Attack;
    }

    private void SetAimDirection(Vector2 direction)
    {
        _aimDirection = direction;
    }

    protected virtual void Attack()
    {
        // 캐릭터 공격 방식에 따라서 분기 나눠서 호출.
    }
    private void MelleAttack()
    {

    }
    private void RangedAttack()
    {

    }
}