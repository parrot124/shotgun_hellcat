using Game.Scripts.Player.StateMachine.States;
using UnityEngine;

namespace Game.Scripts.Player.StateMachine
{
    public abstract class PlayerState : IPlayerState, IPlayerCameraHandler
    {
        public abstract void Enter();

        public abstract void Exit();

        public abstract PhysicsConfig GetConfig();

        public abstract void Update(ref Vector3 velocity, Vector3 wishDirection);

        public abstract void UpdateCamera(ref Transform cameraTransform, Vector3 velocity);
    }
}