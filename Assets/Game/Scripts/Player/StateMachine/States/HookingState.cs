using Game.Player.Abilities;
using Game.Player.Camera;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Player.StateMachine.States
{
    public class HookingState : PlayerState
    {
        [Inject] private HookingStateConfig hookingStateConfig;
        [Inject] private CameraSettings cameraSettings;

        private float timer;
        private float currentRoll;
        private bool isHooked;
        
        public override void Enter(PlayerContext context)
        {
            Debug.Log("Entering HookingState");
        }

        public override void Enter()
        {
            throw new System.NotImplementedException();
        }

        public override void Exit()
        {
            Debug.Log("Exiting HookingState");
        }

        public override PhysicsConfig GetConfig()
        {
            return hookingStateConfig;
        }

        public override void Update(ref Vector3 velocity, Vector3 wishDirection)
        {
            
        }

        public override void UpdateCamera(Transform cameraTransform, ref float xRotation, Vector2 currentMouseDelta, Vector3 velocity)
        {
            float strafeVel = Vector3.Dot(velocity, cameraTransform.parent.right);
            CameraHelper.ApplyStrafeRoll(null, strafeVel, cameraSettings, ref currentRoll);
        }
    }
}