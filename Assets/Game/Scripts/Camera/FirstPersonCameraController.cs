using Game.Common.Interfaces;
using UnityEngine;
using Zenject;

public class FirstPersonCameraController : MonoBehaviour
{
    private Transform player;
    private IPlayerInput playerInput;
    private Transform rig;

    [Inject]
    private void Construct(PlayerController controller, IPlayerInput input)
    {
        player = controller.gameObject.transform;
        playerInput = input;

        input.MouseMove += MouseMoveHandler;
        rig = transform.GetChild(0);
    }

    private void MouseMoveHandler(Vector2 vector)
    {
        float vertical = vector.y;
        float horizontal = vector.x;

        transform.Rotate(0, 0, vertical);
        rig.Rotate(0, horizontal, 0);
    }

    private void Update()
    {
        transform.position = player.position;
    }
}
