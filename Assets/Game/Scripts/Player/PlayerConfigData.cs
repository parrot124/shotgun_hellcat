using UnityEngine;

namespace Game.Player
{
    [CreateAssetMenu(menuName = "Scriptable Object/Player Config Data")]
    public class PlayerConfigData : ScriptableObject
    {
        [SerializeField] public float DefaultSpeed;
        [SerializeField] public float MaxHP;
        [SerializeField] public float MaxMana;
        [SerializeField] public float MouseSensitivity;
        
        [SerializeField] public float JumpForce;
        [SerializeField] public float GravityMultiplier;
        
        [SerializeField] public float DashForce;
        [SerializeField] public float DashCooldown;
    }
}