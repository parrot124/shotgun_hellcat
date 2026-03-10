using UnityEngine;

namespace Game.Player.Camera
{
    public static class CameraHelper
    {
        public static void ApplyHeadBob(Transform cameraPivot, Vector3 velocity, ref float timer, CameraSettings settings)
        {
            Vector3 localPos =  cameraPivot.localPosition;

            if (velocity.magnitude < 0.1f)
            {
                timer = 0f;
                localPos.y = Mathf.Lerp(localPos.y, settings.Midpoint, Time.deltaTime * 8f);
            }
            else
            {
                timer += settings.BobSpeed * velocity.magnitude;
                float waveslice = Mathf.Sin(timer);
                
                float translate = waveslice * settings.BobAmount * Mathf.Clamp01(velocity.magnitude / settings.MaxSpeed);
                localPos.y = settings.Midpoint + translate;
            }
            
            cameraPivot.localPosition = localPos;
        }
        
        //TODO:
        //ApplyStrafeRoll
        //LandingKick
        //HookingFOVBoost
    }
}