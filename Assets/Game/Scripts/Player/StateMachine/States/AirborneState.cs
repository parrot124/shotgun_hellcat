using Game.Player.Abilities;
using Game.Player.Camera;
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
        private float currentRoll;

        public override void Update(ref Vector3 velocity, Vector3 wishDirection)
        {
            Accelerate(ref velocity, wishDirection, playerPhysicsConfig.Acceleration, playerPhysicsConfig.MaxSpeed);
            ApplyGravity(ref velocity, playerPhysicsConfig.Gravity);
        }

        public override void UpdateCamera(Transform cameraTransform, ref float xRotation, Vector2 mouseDelta, Vector3 velocity)
        {
            float strafeVel = Vector3.Dot(velocity, cameraTransform.parent.right);
            
            CameraHelper.ApplyStrafeRoll(cameraTransform, strafeVel, cameraSettings, ref currentRoll);
            CameraHelper.ApplyHeadBob(cameraTransform, velocity, ref cameraTimer, cameraSettings);
            CameraHelper.RotateCamera(cameraTransform, mouseDelta, cameraSettings, ref xRotation);
            
            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0, currentRoll);
        }

        public override void Enter(PlayerContext context)
        {
            Debug.Log("Entered AirborneState");
        }

        public override void Enter()
        {
            //throw new System.NotImplementedException();
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
