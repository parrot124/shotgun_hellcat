using System.Collections;
using Game.Common.Interfaces;
using Game.Player;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private MeshRenderer meshRenderer;
    
    [Header("Debug")]
    [SerializeField] private bool showDebugInfo = false;
    [SerializeField] private bool isGrounded;
    [SerializeField] private Vector2 moveVector;
    [SerializeField] private bool canDash = true;

    // Dependencies
    private IPlayerInput playerInput;
    private PlayerConfig config;
    
    // Constants
    private const float GroundCheckDistance = 0.1f;
    
    [Inject]
    private void Construct(IPlayerInput input)
    {
        playerInput = input;
    }

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();
        config = GetComponent<PlayerConfig>();
        
        canDash = true;
    }

    private void OnEnable()
    {
        playerInput.Move += MoveHandler;
        playerInput.Jump += JumpHandler;
        playerInput.MouseMove += MouseMoveHandler;
        playerInput.Dash += DashHandler;
    }

    private void FixedUpdate()
    {
        GroundedCheck();
        ApplyMovement();
        ApplyGravity();
    }

    private void ApplyGravity()
    {
        if (!isGrounded) rb.AddForce(Physics.gravity * (rb.mass * config.GravityMultiplier), ForceMode.Force);
    }

    private void GroundedCheck()
    {
        float rayLength = Mathf.Abs(meshRenderer.bounds.min.y - transform.position.y) + GroundCheckDistance;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, rayLength);
        
        if (showDebugInfo)
        {
            Debug.DrawRay(transform.position, Vector3.down * rayLength, isGrounded ? Color.green : Color.red);
        }
    }

    private void ApplyMovement()
    {
        if (moveVector.sqrMagnitude > 0.01f)
        {
            Vector3 moveDirection = transform.forward * moveVector.y + transform.right * moveVector.x;
            rb.AddForce(moveDirection.normalized * config.Speed, ForceMode.Acceleration);
        }
    }
    
    private void JumpHandler()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * config.JumpForce, ForceMode.Impulse);
            
            if (showDebugInfo)
            {
                Debug.Log($"Прыжок выполнен! Игрок на земле: {isGrounded}");
            }
        }
    }
    
    private void MoveHandler(Vector2 vector)
    {
        moveVector = vector;
    }
    
    private void MouseMoveHandler(Vector2 vector)
    {
        float angle = vector.x * config.MouseSensitivity;
        rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles.x, rb.rotation.eulerAngles.y + angle, rb.rotation.eulerAngles.z);
    }
    
    private void DashHandler()
    {
        if (!canDash) return;
        
        rb.AddForce(transform.forward * config.DashForce, ForceMode.Impulse);
        
        StartCoroutine(DashCooldown());
        
        if (showDebugInfo)
        {
            Debug.Log($"Рывок выполнен в направлении: {transform.forward}");
        }
    }

    private IEnumerator DashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(config.DashCooldown);
        canDash = true;
    }

    private void OnDisable()
    {
        playerInput.Move -= MoveHandler;
        playerInput.Jump -= JumpHandler;
        playerInput.MouseMove -= MouseMoveHandler;
        playerInput.Dash -= DashHandler;
    }

}