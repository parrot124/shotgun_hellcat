using Game.Common.Interfaces;
using UnityEngine;
using Zenject;

public class FirstPersonCameraController : MonoBehaviour
{

    private Transform player;
    private IPlayerInput playerInput;
    private CameraConfig config;
    private Transform rig;

    private float verticalEulerAngle;

    [Inject]
    private void Construct(PlayerController controller, IPlayerInput input)
    {
        player = controller.gameObject.transform;
        playerInput = input;

        input.MouseMove += MouseMoveHandler;
        rig = transform.parent;
    }

    private void Start()
    {
        config = GetComponent<CameraConfig>();

        verticalEulerAngle = 0f;
    }

    private void Update()
    {
        transform.position = player.position;
    }

    private void MouseMoveHandler(Vector2 vector)
    {
        verticalEulerAngle += -vector.y * config.MouseSensitivity * Time.deltaTime;
        float horizontal = vector.x * config.MouseSensitivity * Time.deltaTime;

        rig.Rotate(0, horizontal, 0);
        transform.rotation = Quaternion.Euler(Mathf.Clamp(verticalEulerAngle, -90f, 90f), transform.rotation.eulerAngles.y, 0);

        Debug.Log(transform.localRotation.eulerAngles.x);
    }
}
