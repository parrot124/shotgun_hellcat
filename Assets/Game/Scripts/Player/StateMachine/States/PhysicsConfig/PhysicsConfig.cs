using UnityEngine;

namespace Game.Scripts.Player.StateMachine.States
{
    [CreateAssetMenu(menuName = "Player/PhysicsConfig")]
    public class PhysicsConfig : ScriptableObject
    {
        [SerializeField] protected float friction;
        [SerializeField] protected float maxSpeed;
        [SerializeField] protected float acceleration;
        [SerializeField] protected float gravity;
        [SerializeField] protected float jumpImpulse;
        [SerializeField] protected float stopSpeed;

        public float Friction => friction;
        public float MaxSpeed => maxSpeed;
        public float Acceleration => acceleration;
        public float Gravity => gravity;
        public float JumpImpulse => jumpImpulse;
        public float StopSpeed => stopSpeed;
    }
}