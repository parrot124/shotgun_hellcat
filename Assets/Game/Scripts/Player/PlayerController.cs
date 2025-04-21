using Game.Common.Interfaces;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private IPlayerInput playerInput;

    [Inject]
    private void Construct(IPlayerInput input)
    {
        playerInput = input;
        playerInput.MouseMove += MoveHandler;
    }

    private void MoveHandler(Vector2 vector)
    {
        Debug.Log("MovementHadler invoked");
        characterController.attachedRigidbody.AddForce(vector, ForceMode.Force);   
    }
}
