using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownPlayerController : TopDownCharacterController
{
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }
    private void OnMove(InputValue value)
    {
        Vector2 direction = value.Get<Vector2>();
        direction = direction.normalized;
        CallMoveEvent(direction);
    }

    private void OnLook(InputValue value)
    {
        Vector2 mousePosition = value.Get<Vector2>();
        mousePosition = _camera.ScreenToWorldPoint(mousePosition);
        CallLookEvent((mousePosition - (Vector2)transform.position).normalized);
    }

    private void OnAttack(InputValue value)
    {
        IsAttacking = value.isPressed;
    }

    private void OnInteract(InputValue value)
    {

    }
    private void OnOpenPlayerStatusUI(InputValue value)
    {
        if (value.isPressed)
        {
            if (UIManager.Instance.IsOpenedUI(UIType.PlayerStatus))
            {
                UIManager.Instance.CloseUI(UIType.PlayerStatus);
                return;
            }
            UIManager.Instance.OpenUI(UIType.PlayerStatus);
        }
    }
}
