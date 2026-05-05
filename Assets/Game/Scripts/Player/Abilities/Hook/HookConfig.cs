using UnityEngine;

namespace Game.Player.Abilities
{
    [CreateAssetMenu(menuName = "Player Abilities/Hook Config")]
    public class HookConfig : ScriptableObject
    {
        [SerializeField] protected float pullStrength;
        [SerializeField] protected float maxPullSpeed;
        [SerializeField] protected float maxHookDistance;
        
        public float PullStrength => pullStrength;
        public float MaxPullSpeed => maxPullSpeed;
        public float MaxHookDistance => maxHookDistance;
    }
}