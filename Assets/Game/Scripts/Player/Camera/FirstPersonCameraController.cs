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
    private float defaultFOV;
    
    [SerializeField] float fovMultiplier;
    [SerializeField] [Range(0f, 1f)] float interpolationValue;

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
        rig.SetParent(player);

        rig.position = player.position;
    }

    private void Start()
    {
        config = GetComponent<CameraConfig>();
        verticalEulerAngle = 0f;
        
        defaultFOV = this.GetComponent<Camera>().fieldOfView;
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, rig.forward, Color.yellow, 0.1f);

        Vector3 groundVelocity = transform.parent.parent.GetComponent<Rigidbody>().linearVelocity;
        groundVelocity.y = 0;
        
        float newFOV = Mathf.Lerp(defaultFOV, defaultFOV + groundVelocity.magnitude * fovMultiplier, interpolationValue);
        GetComponent<Camera>().fieldOfView = newFOV;
    }

    private void MouseMoveHandler(Vector2 vector)
    {
        verticalEulerAngle = Mathf.Clamp(verticalEulerAngle + -vector.y * config.MouseSensitivity, -90f, 90f);
        //float horizontal = vector.x * config.MouseSensitivity * Time.deltaTime;

        transform.rotation = Quaternion.Euler(verticalEulerAngle, transform.rotation.eulerAngles.y, 0);
    }
}
