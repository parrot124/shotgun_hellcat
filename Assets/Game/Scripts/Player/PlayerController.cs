using System;
using Game.Player;
using Game.Player.Camera;
using Game.Scripts;
using Game.Scripts.Player.Input.Common;
using Game.Scripts.Player.StateMachine;
using Game.Scripts.Player.StateMachine.States;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour, ISnapshotable
{
    public event Action<bool> OnGroundedChanged;
    
    [Header("Components")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private PlayerContext context;
    
    // Dependencies
    private IPlayerInput playerInput;
    private PlayerConfig config;
    private CameraSettings cameraSettings;
    
    // Runtime fields
    private Vector3 wishDirection;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isJumpButtonHolding;
    
    // State-Dependent fields
    private PhysicsConfig playerPhysicsConfig;
    private PlayerStateMachine stateMachine;
    
    [Inject]
    private void Construct(IPlayerInput input, PlayerStateMachine stateMachine, CameraSettings cameraSettings)
    {
        playerInput = input;
        this.stateMachine = stateMachine;
        this.cameraSettings = cameraSettings;
        
        config = GetComponent<PlayerConfig>();
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        Vector3 globalWishDirection = transform.right * wishDirection.x + transform.forward * wishDirection.y;  

        stateMachine.Tick(ref velocity, globalWishDirection.normalized, context);

        controller.Move(velocity * Time.fixedDeltaTime);
        
        if (isJumpButtonHolding) JumpHandler();
    }
    
    //Handlers
    private void MoveHandler(Vector2 moveVector) => wishDirection = new Vector3(moveVector.x, moveVector.y, 0);
    
    private void JumpHandler() => stateMachine.TryJump(ref velocity);

    private void OnJumpReleased() => isJumpButtonHolding = false;

    private void OnJumpPressed() => isJumpButtonHolding = true;

    private void HookHandler()
    {
        Ray hookRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(hookRay, out RaycastHit hit, config.HookDistance);

        stateMachine.TryHook(hit);
    }

    private void MouseMoveHandler(Vector2 mouseDelta)
    {
        float rotationY = mouseDelta.x * cameraSettings.Sensitivity;
        transform.Rotate(0, rotationY, 0);
    }

    //OnEnable/Disable   
    private void OnEnable()
    {
        playerInput.Move += MoveHandler;
        playerInput.MouseMove += MouseMoveHandler;
        playerInput.Jump += OnJumpPressed;
        playerInput.JumpCanceled += OnJumpReleased;
        playerInput.Hook += HookHandler;
    }
    
    private void OnDisable()
    {
        playerInput.Move -= MoveHandler;
        playerInput.MouseMove -= MouseMoveHandler;
        playerInput.Jump -= JumpHandler;
        playerInput.Hook -= HookHandler;
    }
}