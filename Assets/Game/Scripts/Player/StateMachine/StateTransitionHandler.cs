using Game.Scripts.Player.StateMachine;
using Game.Scripts.Player.StateMachine.States;
using Zenject;

public class StateTransitionHandler
{
    private PlayerStateMachine stateMachine;
    private TriggerHub triggerHub;

    [Inject]
    public StateTransitionHandler(PlayerStateMachine stateMachine, TriggerHub triggerHub)
    {
        this.stateMachine = stateMachine;
        this.triggerHub = triggerHub;

        triggerHub.OnGroundedChanged += HandleGroundChanged;
        triggerHub.OnJumpPressed += HandleJumpPressed;
    }

    private void HandleJumpPressed()
    {
        
    }

    private void HandleGroundChanged(bool grounded)
    {
        if (grounded)
            stateMachine.SetState<GroundedState>();
        if (grounded == false)
            stateMachine.SetState<AirborneState>();
    }
}   