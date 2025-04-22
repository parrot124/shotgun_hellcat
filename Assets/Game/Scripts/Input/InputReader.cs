using System;
using Game.Common.Interfaces;
using Game.Input;
using Unity.VisualScripting.FullSerializer;
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
    public InputReader(GameInput input)
    {
        Debug.Log("InputReader installer");

        gameInput = input;
        gameInput.Enable();

        gameInput.Gameplay.CameraRotation.performed += OnMouseMove;
        gameInput.Gameplay.Movement.performed += OnMovementPress;
        gameInput.Gameplay.Movement.canceled += OnMovementPress;
    }

    private void OnMovementPress(InputAction.CallbackContext context)
    {
        Debug.Log("Movement pressed");

        Move?.Invoke(context.ReadValue<Vector2>());
    }

    private void OnMouseMove(InputAction.CallbackContext context)
    {
        MouseMove?.Invoke(context.ReadValue<Vector2>());
    }
}
