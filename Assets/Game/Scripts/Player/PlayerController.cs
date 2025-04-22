using System;
using Game.Common.Interfaces;
using Game.Player;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private IPlayerInput playerInput;
    private PlayerConfig config;

    private Vector2 moveVector;

    [Inject]
    private void Construct(IPlayerInput input)
    {
        playerInput = input;
        playerInput.Move += MoveHandler;
        playerInput.MouseMove += MouseMoveHandler;
    }

    private void OnEnable()
    {
        characterController = GetComponent<CharacterController>();
        config = GetComponent<PlayerConfig>();
    }

    private void Update()
    {
        DoMove();
    }

    private void DoMove()
    {
        Vector3 move = transform.forward * moveVector.y + transform.right * moveVector.x;
        characterController.Move(move * Time.deltaTime);
    }

    private void MoveHandler(Vector2 vector)
    {
        moveVector = vector;
    }

    private void MouseMoveHandler(Vector2 vector)
    {
        float horizontal = vector.x;
        transform.Rotate(0, config.MouseSensitivity * horizontal * Time.deltaTime, 0);
    }
}
