using UnityEngine;

namespace Game.Player
{
    [CreateAssetMenu(menuName = "Scriptable Object/Player Config Data")]
    public class PlayerConfigData : ScriptableObject
    {
        [SerializeField] public float MaxHP;
        [SerializeField] public float MaxMana;
        [SerializeField] public float MouseSensitivity;
        [SerializeField] public float HookDistance;
    }
}