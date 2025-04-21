using System;
using Game.Common.Interfaces;
using Game.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputReader : IPlayerInput
{
    public event Action<Vector2> MouseMove;
    public event Action<Vector2> Move;

    public event Action Jump;
    public event Action Sneak;
    public event Action Pause;

    private GameInput gameInput;

    [Inject]
    public InputReader(LazyInject<GameInput> input)
    {
        gameInput = input.Value;
        gameInput.Enable();

        gameInput.Gameplay.CameraRotation.performed += OnMouseMove;
        gameInput.Gameplay.Movement.performed += OnMovementPerformed;
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("OnMovementPerformed invoked");
        Move?.Invoke(context.ReadValue<Vector2>());
    }

    private void OnMouseMove(InputAction.CallbackContext context)
    {
        MouseMove.Invoke(context.ReadValue<Vector2>());
    }
}
