using UnityEngine;

namespace Game.Player
{
    public class PlayerConfig : MonoBehaviour
    {
        public float Speed => speed;
        public float MaxHP => maxHP;
        public float MaxMana => maxMana;
        public float MouseSensitivity => mouseSensitivity;

        [SerializeField] private float speed;
        [SerializeField] private float maxHP;
        [SerializeField] private float maxMana;
        [SerializeField] private float mouseSensitivity;
    }
}
