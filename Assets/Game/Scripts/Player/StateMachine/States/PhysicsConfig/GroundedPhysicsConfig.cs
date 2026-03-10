using UnityEngine;

namespace Game.Scripts.Player.StateMachine.States
{
    [CreateAssetMenu(menuName = "Player/PhysicsConfig/GroundedPhysicsConfig")]
    public class GroundedPhysicsConfig : PhysicsConfig
    {
        [SerializeField] protected float stepSnapForce;
        public float StepSnapForce => stepSnapForce;
    }
}