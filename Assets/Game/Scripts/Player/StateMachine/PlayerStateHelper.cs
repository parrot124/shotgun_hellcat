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
        
        public static void AirControl(ref Vector3 velocity, Vector3 wishDir, float airControl, float airAccel)
        {
            if (wishDir.magnitude < 0.001f) return;

            float ySpeed = velocity.y;           // сохраняем вертикаль
            velocity.y = 0;                      // работаем только с горизонтальной скоростью

            float speed = velocity.magnitude;
            if (speed < 0.001f)
            {
                velocity.y = ySpeed;
                return;
            }

            velocity.Normalize();                // теперь velocity — unit vector

            float dot = Vector3.Dot(velocity, wishDir);
            if (dot > 0)                         // только если движемся примерно в нужном направлении
            {
                float k = airControl * dot * dot * Time.fixedDeltaTime * airAccel;
                velocity += k * wishDir;
                velocity.Normalize();
            }

            velocity *= speed;                   // возвращаем прежнюю скорость
            velocity.y = ySpeed;                 // восстанавливаем Y
        }
    }
}
