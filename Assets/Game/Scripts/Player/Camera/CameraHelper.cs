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

        public static void RotateCamera(Transform cameraPivot, Vector2 mouseDelta, CameraSettings config, ref float xRotation, float minAngle = -90, float maxAngle = 90)
        {
            xRotation -= mouseDelta.y * config.Sensitivity;
            xRotation = Mathf.Clamp(xRotation, minAngle, maxAngle);
            
            cameraPivot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
        
        public static void ApplyStrafeRoll(Transform cameraPivot, float strafeVelocity, 
            CameraSettings config, ref float currentRoll)
        {
            float targetRoll = -strafeVelocity * config.StrafeRollIntensity;
            currentRoll = Mathf.Lerp(currentRoll, targetRoll, config.RollLerpSpeed * Time.deltaTime);
        }
        
        //TODO:
        //ApplyStrafeRoll
        //LandingKick
        //HookingFOVBoost
    }
}