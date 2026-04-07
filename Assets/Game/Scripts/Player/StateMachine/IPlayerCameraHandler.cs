using Game.Player.Camera;
using UnityEngine;

namespace Game.Scripts.Player.StateMachine
{
    public interface IPlayerCameraHandler
    {
        public void UpdateCamera(Transform cameraTransform, ref float xRotation, Vector2 mouseDelta, Vector3 velocity);
    }
}