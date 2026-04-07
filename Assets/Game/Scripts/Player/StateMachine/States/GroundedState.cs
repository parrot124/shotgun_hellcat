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
        [Inject] private HookConfig hookConfig;
        
        private int frictionDelay;
        private float cameraTimer;
        private float currentRoll;

        private bool isHooking;
        private float hookingTime;
        private Vector3 hookPoint;

        public override void Update(ref Vector3 velocity, Vector3 wishDirection)
        {
            if (frictionDelay < 0)
                ApplyFriction(ref velocity, playerPhysicsConfig.Friction, playerPhysicsConfig.StopSpeed);
            else frictionDelay--;
            
            Accelerate(ref velocity, wishDirection.normalized, playerPhysicsConfig.Acceleration, playerPhysicsConfig.MaxSpeed);
            SnapToGround(ref velocity, playerPhysicsConfig.StepSnapForce);
            
            if (isHooking)
            {
                ApplyHookPull(ref velocity, hookPoint, Camera.main.transform.parent.parent, hookConfig, ref isHooking, ref hookPoint);
            }
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

        public override void TryHook()
        {
            if (TryStartHook(Camera.main.transform, 1 << LayerMask.NameToLayer("Ground"), hookConfig, out hookPoint))
            {
                isHooking = true;
                hookingTime = Time.time;
            };
        }
    }
}
