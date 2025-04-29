using System;
using Game.Common.Interfaces;
using UnityEngine;
using Zenject;

public class FirstPersonCameraController : MonoBehaviour
{
    //dependencies
    private IPlayerInput playerInput;
    private CameraConfig config;

    private Transform player;
    private Transform rig;

    private float verticalEulerAngle;

    [Inject]
    private void Construct(PlayerController controller, IPlayerInput input)
    {
        player = controller.gameObject.transform;
        playerInput = input;
        

    }

    private void OnEnable()
    {
        playerInput.MouseMove += MouseMoveHandler;
        rig = transform.parent;
        rig.LookAt(player.forward);
    }

    private void Start()
    {
        config = GetComponent<CameraConfig>();
        verticalEulerAngle = 0f;
    }

    private void Update()
    {
        rig.position = player.position;
        rig.LookAt(player.transform.forward);

        Debug.DrawRay(transform.position, rig.forward, Color.yellow, 0.1f);
    }

    private void MouseMoveHandler(Vector2 vector)
    {
        verticalEulerAngle = Mathf.Clamp(verticalEulerAngle + -vector.y * config.MouseSensitivity, -90f, 90f);
        //float horizontal = vector.x * config.MouseSensitivity * Time.deltaTime;

        transform.rotation = Quaternion.Euler(verticalEulerAngle, transform.rotation.eulerAngles.y, 0);
    }
}
