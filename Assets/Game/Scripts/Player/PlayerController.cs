using System;
using Game.Player;
using Game.Player.Camera;
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
        GroundedCheck();

        Vector3 globalWishDirection = transform.right * wishDirection.x + transform.forward * wishDirection.y;  

        stateMachine.Tick(ref velocity, globalWishDirection.normalized);

        controller.Move(velocity * Time.fixedDeltaTime);
        
        if (isJumpButtonHolding) JumpHandler();
        
        Debug.DrawRay(transform.position, globalWishDirection, Color.green);
        Debug.DrawRay(transform.position, velocity, Color.blue);
    }

    private void GroundedCheck()
    {
        bool prevGrounded = isGrounded;
        
        Ray ray = new Ray(transform.position, Vector3.down);
        isGrounded = Physics.SphereCast(ray,controller.radius,controller.height/2.0f + 0.1f, 1 << LayerMask.NameToLayer("Ground"));

        if (prevGrounded != isGrounded)
        {
            OnGroundedChanged?.Invoke(isGrounded);
        }
    }
    
    private void MoveHandler(Vector2 moveVector)
    {
        wishDirection = new Vector3(moveVector.x, moveVector.y, 0);
    }

    private void JumpHandler()
    {
        stateMachine.TryJump(ref velocity);
    }
    
    private void OnEnable()
    {
        playerInput.Move += MoveHandler;
        playerInput.MouseMove += MouseMoveHandler;
        playerInput.Jump += OnJumpPressed;
        playerInput.JumpCanceled += OnJumpReleased;
        playerInput.Hook += HookHandler;
    }

    private void OnJumpReleased()
    {
        isJumpButtonHolding = false;
    }

    private void OnJumpPressed()
    {
        isJumpButtonHolding = true;
    }

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

    private void OnDisable()
    {
        playerInput.Move -= MoveHandler;
        playerInput.MouseMove -= MouseMoveHandler;
        playerInput.Jump -= JumpHandler;
        playerInput.Hook -= HookHandler;
    }
    
    private void OnGUI()
    {
        float hSpeed = new Vector3(velocity.x, 0, velocity.z).magnitude;
        GUILayout.Label($"Speed: {hSpeed:F2}", GUILayout.Height(300), GUILayout.Width(300));
    }
}