using UnityEngine;

namespace Game.Scripts.Player.StateMachine.States
{
    [CreateAssetMenu(menuName = "Player/PhysicsConfig/AirbornePhysicsConfig")]
    public class AirbornePhysicsConfig : PhysicsConfig
    {
        [SerializeField] protected float airControl;
    
        public float AirControl => airControl;
    }
}