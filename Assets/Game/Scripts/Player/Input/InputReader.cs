using System;
using Game.Input;
using Game.Scripts.Player.Input.Common;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputReader : IPlayerInput
{
    public event Action<Vector2> MouseMove;
    public event Action<Vector2> Move;

    public event Action Jump;
    public event Action JumpCanceled;
    public event Action Sneak;
    public event Action Pause;
    public event Action Hook;

    private GameInput gameInput;

    [Inject]
    public InputReader(GameInput input)
    {
        gameInput = input;
        gameInput.Enable();

        gameInput.Gameplay.CameraRotation.performed += OnMouseMove;
        gameInput.Gameplay.CameraRotation.canceled += OnMouseMove;
        gameInput.Gameplay.Movement.performed += OnMovementPress;
        gameInput.Gameplay.Movement.canceled += OnMovementPress;
        gameInput.Gameplay.Jump.performed += OnJumpPress;
        gameInput.Gameplay.Jump.canceled += OnJumpReleased;
        gameInput.Gameplay.Dash.performed += OnDashPress;
    }

    private void OnJumpReleased(InputAction.CallbackContext obj)
    {
        JumpCanceled?.Invoke();
    }

    private void OnDashPress(InputAction.CallbackContext obj)
    {
        Hook?.Invoke();
    }

    private void OnJumpPress(InputAction.CallbackContext context)
    {
        Jump?.Invoke();
    }

    private void OnMovementPress(InputAction.CallbackContext context)
    {
        Move?.Invoke(context.ReadValue<Vector2>());
    }

    private void OnMouseMove(InputAction.CallbackContext context)
    {
        MouseMove?.Invoke(context.ReadValue<Vector2>());
    }
}
