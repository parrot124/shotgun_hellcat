using Game.Common.Interfaces;
using UnityEngine;
using Zenject;

///needs rework?
public class FirstPersonCameraController : MonoBehaviour
{
    //dependencies
    private IPlayerInput playerInput;
    private CameraConfig config;

    private Transform player;
    private Transform rig;
    
    private CharacterController playerCharacterController;
    private Camera playerCamera;

    private float verticalEulerAngle;
    private float defaultFOV;
    
    [SerializeField] float FOVMultiplier;
    [SerializeField] [Range(0f, 1f)] float lerpValue;

    [Inject]
    private void Construct(PlayerController controller, IPlayerInput input)
    {
        player = controller.gameObject.transform;
        playerInput = input;
        
        config = GetComponent<CameraConfig>();
        playerCamera = GetComponent<Camera>();
        defaultFOV = playerCamera.fieldOfView;
        playerCharacterController = player.GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        playerInput.MouseMove += MouseMoveHandler;
        rig = transform.parent;
        rig.SetParent(player);

        rig.position = player.position;
    }
    
    private void Update()
    {
        Vector3 horizontalVelocity = playerCharacterController.velocity;
        horizontalVelocity.y = 0;
        
        float newFOV = Mathf.Lerp(defaultFOV, defaultFOV + horizontalVelocity.magnitude * FOVMultiplier, lerpValue);
        playerCamera.fieldOfView = newFOV;
    }

    private void MouseMoveHandler(Vector2 vector)
    {
        verticalEulerAngle = Mathf.Clamp(verticalEulerAngle + -vector.y * config.MouseSensitivity, -90f, 90f);
        transform.rotation = Quaternion.Euler(verticalEulerAngle, transform.rotation.eulerAngles.y, 0);
    }

    private void OnDisable()
    {
        playerInput.MouseMove -= MouseMoveHandler;
    }
}
