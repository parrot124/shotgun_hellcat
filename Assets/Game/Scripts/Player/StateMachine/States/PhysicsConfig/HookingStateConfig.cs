using Game.Player.Abilities;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Player.StateMachine.States
{
    [CreateAssetMenu(menuName = "Player/PhysicsConfig/HookingStateConfig")]
    public class HookingStateConfig : PhysicsConfig
    {
        [Inject] public HookConfig hookConfig;
    }
}