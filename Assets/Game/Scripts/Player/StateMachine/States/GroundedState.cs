using Game.Player.Camera;
using UnityEngine;
using Zenject;
using static Game.Scripts.Player.StateMachine.PlayerStateHelper;

namespace Game.Scripts.Player.StateMachine.States
{
    public class GroundedState : PlayerState
    {
        [Inject] private GroundedPhysicsConfig playerPhysicsConfig;
        [Inject] private CameraSettings cameraSettings;
        
        private int frictionDelay;
        private float cameraTimer;
        
        public override void Update(ref Vector3 velocity, Vector3 wishDirection)
        {
            ApplyFriction(ref velocity, playerPhysicsConfig.Friction, playerPhysicsConfig.StopSpeed);
            Accelerate(ref velocity, wishDirection.normalized, playerPhysicsConfig.Acceleration, playerPhysicsConfig.MaxSpeed);
            SnapToGround(ref velocity, playerPhysicsConfig.StepSnapForce);
        }

        public override void UpdateCamera(ref Transform cameraTransform, Vector3 velocity)
        {
            CameraHelper.ApplyHeadBob(cameraTransform, velocity, ref cameraTimer, cameraSettings);
        }

        public override void Enter()
        {
            Debug.Log("Entered GroundedState");
        }

        public override void Exit()
        {
            Debug.Log("Exited GroundedState");
        }

        public override PhysicsConfig GetConfig()
        {
            return playerPhysicsConfig;
        }

        public void TryJump(ref Vector3 velocity)
        {
            PerformJump(ref velocity, playerPhysicsConfig.JumpImpulse);
        }
    }
}
