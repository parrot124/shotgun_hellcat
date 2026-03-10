using Game.Player.Camera;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using Zenject;
using static Game.Scripts.Player.StateMachine.PlayerStateHelper;

namespace Game.Scripts.Player.StateMachine.States
{
    public class AirborneState : PlayerState
    {
        [Inject] private AirbornePhysicsConfig playerPhysicsConfig;
        [Inject] private CameraSettings cameraSettings;

        private float cameraTimer;

        public override void Update(ref Vector3 velocity, Vector3 wishDirection)
        {
            Accelerate(ref velocity, wishDirection, playerPhysicsConfig.Acceleration, playerPhysicsConfig.MaxSpeed);
            ApplyGravity(ref velocity, playerPhysicsConfig.Gravity);
        }

        public override void UpdateCamera(ref Transform cameraTransform, Vector3 velocity)
        {
            CameraHelper.ApplyHeadBob(cameraTransform, velocity, ref cameraTimer, cameraSettings);
        }

        public override void Enter()
        {
            Debug.Log("Entered AirborneState");
        }

        public override void Exit()
        {
            Debug.Log("Exited AirborneState");
        }

        public override PhysicsConfig GetConfig()
        {
            throw new System.NotImplementedException();
        }

        public void TryAirJump(ref Vector3 velocity)
        {
            
        }
    }
}
