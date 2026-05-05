using Game.Player.Abilities;
using Game.Player.Camera;
using UnityEngine;
using UnityEngine.InputSystem;
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
        private float currentRoll;

        public override void Update(ref Vector3 velocity, Vector3 wishDirection)
        {
            if (frictionDelay < 0)
                ApplyFriction(ref velocity, playerPhysicsConfig.Friction, playerPhysicsConfig.StopSpeed);
            else frictionDelay--;
            
            Accelerate(ref velocity, wishDirection.normalized, playerPhysicsConfig.Acceleration, playerPhysicsConfig.MaxSpeed);
            SnapToGround(ref velocity, playerPhysicsConfig.StepSnapForce);
        }

        public override void UpdateCamera(Transform cameraTransform, ref float xRotation, Vector2 mouseDelta, Vector3 velocity)
        {
            float strafeVel = Vector3.Dot(velocity, cameraTransform.parent.right);
            
            CameraHelper.ApplyStrafeRoll(cameraTransform, strafeVel, cameraSettings, ref currentRoll);
            CameraHelper.ApplyHeadBob(cameraTransform, velocity, ref cameraTimer, cameraSettings);
            CameraHelper.RotateCamera(cameraTransform, mouseDelta, cameraSettings, ref xRotation);
            
            cameraTransform.localRotation = Quaternion.Euler(xRotation, 0, currentRoll);
        }

        public override void Enter()
        {
            frictionDelay = 2;
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
