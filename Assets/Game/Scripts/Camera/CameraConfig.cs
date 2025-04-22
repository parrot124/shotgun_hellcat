using UnityEngine;

public class CameraConfig : MonoBehaviour
{
    public float MouseSensitivity => mouseSensitivity;

    [SerializeField] private float mouseSensitivity;
}
