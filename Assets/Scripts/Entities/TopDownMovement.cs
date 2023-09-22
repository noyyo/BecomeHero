using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    private TopDownCharacterController _controller;
    private Rigidbody2D _rigidbody;
    private Vector2 _moveDirection = Vector2.zero;
    private void Awake()
    {
        _controller = GetComponent<TopDownCharacterController>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _controller.OnMoveEvent += SetMoveDirection;
    }

    private void SetMoveDirection(Vector2 direction)
    {
        _moveDirection = direction;
    }

    private void FixedUpdate()
    {
        Move(_moveDirection);
    }

    private void Move(Vector2 direction)
    {
        _rigidbody.velocity = direction * 5; // TODO : 후에 캐릭터별 스탯으로 바꿔야 함
    }
}
