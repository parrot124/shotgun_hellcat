using UnityEngine;

namespace Game.Scripts.Player.StateMachine
{
    public interface IPlayerCameraHandler
    {
        public void UpdateCamera(ref Transform cameraTransform, Vector3 velocity);
    }
}