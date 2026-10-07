using UnityEngine;

namespace Game.Scripts.Core.Extensions.CharacterController
{
    public static class CharacterControllerExtension
    {
        //specific grounded check method
        public static bool IsGroundedSpherecast(this UnityEngine.CharacterController characterController)
        {
            var ray = new Ray(characterController.transform.position, characterController.transform.position + Vector3.down);
            return Physics.SphereCast(ray, characterController.radius, characterController.height / 2f + 0.1f, layerMask: 1 << LayerMask.NameToLayer("Ground"));
        }
    }
}