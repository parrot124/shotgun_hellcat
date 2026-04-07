using Game.Player.Abilities;
using UnityEngine;

namespace Game.Scripts.Player.StateMachine
{
    public static class PlayerStateHelper
    {
        public static void ApplyGravity(ref Vector3 velocity, float gravity)
        {
            velocity.y += gravity * Time.fixedDeltaTime;
        }

        public static void PerformJump(ref Vector3 velocity, float jumpImpulse)
        {
            velocity.y = jumpImpulse;
        }
        
        public static void SnapToGround(ref Vector3 velocity, float stepOffset)
        {
            velocity.y = -stepOffset / Time.fixedDeltaTime;
        }
        
        public static void ApplyFriction(ref Vector3 velocity, float friction, float stopSpeed)
        {
            Vector3 horizontalVelocity = new Vector3(velocity.x, 0, velocity.z);
            float currentSpeed = horizontalVelocity.magnitude;
            if (currentSpeed < 0.01f)
            {
                velocity.x = 0;
                velocity.z = 0;
                return;
            }

            float control = Mathf.Min(currentSpeed, stopSpeed);
            float drop = control * friction * Time.fixedDeltaTime;
            float newSpeed = Mathf.Max(0, currentSpeed - drop);
        
            velocity.x *= newSpeed / currentSpeed;
            velocity.z *= newSpeed / currentSpeed;
        }
        
        public static void Accelerate(ref Vector3 velocity, Vector3 wishDir, float acceleration, float wishSpeed)
        {
            float proj = Vector3.Dot(velocity, wishDir);
            float add = wishSpeed - proj;

            if (add <= 0) return;

            float accAmount = acceleration * Time.fixedDeltaTime * wishSpeed;
            accAmount = Mathf.Min(add, accAmount);
            velocity += wishDir * accAmount;
        }
        
        public static void ApplyHookPull(ref Vector3 velocity, Vector3 hookPoint, Transform playerTransform,
            HookConfig config, ref bool isHooked, ref Vector3 lastHookPoint)
        {
            if (!isHooked) return;

            Vector3 toHook = hookPoint - playerTransform.position;
            float distance = toHook.magnitude;

            if (distance < 3f)
            {
                isHooked = false;
                return;
            }

            Vector3 pullDir = toHook.normalized;
            float pullForce = config.PullStrength * (distance / config.MaxHookDistance);

            velocity = Vector3.Lerp(velocity, pullDir * config.MaxPullSpeed, pullForce * Time.deltaTime);
        
            lastHookPoint = hookPoint;
        }
        
        public static bool TryStartHook(Transform cameraTransform, LayerMask hookableMask,
            HookConfig config, out Vector3 hitPoint)
        {
            hitPoint = Vector3.zero;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward,
                    out RaycastHit hit, config.MaxHookDistance, hookableMask))
            {
                hitPoint = hit.point;
                return true;
            }
            return false;
        }
        
        public static void AirControl(ref Vector3 velocity, Vector3 wishDir, float airControl, float airAccel)
        {
            if (wishDir.magnitude < 0.001f) return;

            float ySpeed = velocity.y;           
            velocity.y = 0;                      

            float speed = velocity.magnitude;
            if (speed < 0.001f)
            {
                velocity.y = ySpeed;
                return;
            }

            velocity.Normalize();                

            float dot = Vector3.Dot(velocity, wishDir);
            if (dot > 0)                         
            {
                float k = airControl * dot * dot * Time.fixedDeltaTime * airAccel;
                velocity += k * wishDir;
                velocity.Normalize();
            }

            velocity *= speed;                   
            velocity.y = ySpeed;                 
        }
    }
}
