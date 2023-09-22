using System;
using UnityEngine;

public class TopDownAimRotation : MonoBehaviour
{
    private TopDownCharacterController _controller;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _controller = GetComponent<TopDownCharacterController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _controller.OnLookEvent += Look;
    }

    private void Look(Vector2 direction)
    {
        float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (Mathf.Abs(rotationZ) > 90)
            _spriteRenderer.flipX = true;
        else
            _spriteRenderer.flipX = false;
    }
}
