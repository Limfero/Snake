using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    private PlayerInput _input;

    public event Action<Vector2> Direction;

    private void Awake()
    {
        _input = new PlayerInput();

        _input.Player.Movement.performed += OnMove;
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 direction = context.action.ReadValue<Vector2>();
        Direction?.Invoke(direction);
    }
}
