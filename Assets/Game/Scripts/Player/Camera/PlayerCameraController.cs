using System;
using Game.Player.Camera;
using Game.Scripts.Player.Input.Common;
using Game.Scripts.Player.StateMachine;
using UnityEngine;
using Zenject;

public class PlayerCameraController : MonoBehaviour
{
    //dependencies
    private IPlayerInput input;
    private PlayerStateMachine stateMachine;
    private Transform cameraTransform;
    private Transform playerTransform;
    private CameraSettings cameraSettings;
    
    //runtime fields
    private Vector2 currentMouseDelta;
    private float xRotation;
    
    [Inject]
    private void Construct(IPlayerInput playerInput, PlayerStateMachine playerStateMachine, PlayerController playerController, CameraSettings cameraSettings)
    {
        input = playerInput;
        stateMachine = playerStateMachine;
        playerTransform = playerController.transform;
    }

    private void OnEnable()
    {
        input.MouseMove += OnMouseMove;
    }

    private void OnMouseMove(Vector2 mouseDelta)
    {
        currentMouseDelta = mouseDelta;
        print(mouseDelta);
    }

    private void Start()
    {
        cameraTransform = Camera.main.GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
        
        cameraTransform.SetParent(transform, false);
        transform.SetParent(playerTransform, false);
    }

    private void LateUpdate()
    {
        stateMachine.CameraTick(cameraTransform, currentMouseDelta, ref xRotation);
    }
}
