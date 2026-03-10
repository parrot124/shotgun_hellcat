using System;
using Game.Common.Interfaces;
using Game.Scripts.Player.StateMachine;
using UnityEngine;
using Zenject;

public class PlayerCameraController : MonoBehaviour
{
    private IPlayerInput input;
    private PlayerStateMachine stateMachine;
    private Transform cameraTransform;
    
    [Inject]
    private void Construct(IPlayerInput playerInput, PlayerStateMachine playerStateMachine)
    {
        input = playerInput;
        stateMachine = playerStateMachine;
    }

    private void Start()
    {
        cameraTransform = Camera.main.GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        stateMachine.CameraTick(ref cameraTransform);
    }
}
