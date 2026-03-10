using UnityEngine;

namespace Game.Player.Camera
{
    [CreateAssetMenu(menuName = "Camera/CameraSettings")]
    public class CameraSettings : ScriptableObject
    {
        [SerializeField] protected float bobSpeed;
        [SerializeField] protected float bobAmount;
        [SerializeField] protected float midpoint;
        [SerializeField] protected float maxSpeed;
        [SerializeField] protected float landingKick;

        public float BobSpeed => bobSpeed;
        public float BobAmount => bobAmount;
        public float Midpoint => midpoint;
        public float MaxSpeed => maxSpeed;
        public float LandingKick => landingKick;
    }
}