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
    }

    private void OnEnable()
    {
        characterController = GetComponent<CharacterController>();
        config = GetComponent<PlayerConfig>();
    }

    private void Update()
    {
        characterController.Move(moveVector * Time.deltaTime * config.Speed);
    }

    private void MoveHandler(Vector2 vector)
    {
        moveVector = vector;
    }
}
