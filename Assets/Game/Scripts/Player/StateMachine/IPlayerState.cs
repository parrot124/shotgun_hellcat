using Game.Scripts.Player.StateMachine.States;
using UnityEngine;

namespace Game.Scripts.Player.StateMachine
{
    public interface IPlayerState
    {
        public void Enter(PlayerContext context);
        public void Exit();
        public PhysicsConfig GetConfig();

        public void Update(ref Vector3 velocity, Vector3 wishDirection);

    }
}