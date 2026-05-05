using System;
using Game.Scripts.Player.Input.Common;
using Zenject;

public class TriggerHub
{
    public event Action<bool> OnGroundedChanged;
    public event Action OnJumpPressed;
    public event Action OnHookPressed;

    [Inject]
    public TriggerHub(PlayerController controller, IPlayerInput playerInput)
    {
        controller.OnGroundedChanged += InvokeGroundedChanged;
        
        playerInput.Jump += JumpPressedHandler;
        playerInput.Hook += HookPressedHandler;
    }

    private void HookPressedHandler()
    {
        OnHookPressed?.Invoke();
    }

    private void JumpPressedHandler()
    {
        OnJumpPressed?.Invoke();
    }

    private void InvokeGroundedChanged(bool isGrounded)
    {
        OnGroundedChanged?.Invoke(isGrounded);
    }
}
