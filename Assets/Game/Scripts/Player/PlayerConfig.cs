using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class PlayerConfig : MonoBehaviour
    {
        private bool isConstructed;
        
        [Inject]
        private void Construct(PlayerConfigData data)
        {
            print("PlayerConfig constructed");

            maxHP = data.MaxHP;
            maxMana = data.MaxMana;
            mouseSensitivity = data.MouseSensitivity;
            hookDistance = data.HookDistance;
            
            isConstructed = true;
        }

        public float MaxHP => isConstructed 
            ? maxHP 
            : throw new UnassignedReferenceException("PlayerConfig isn't constructed before accessing it");
        public float MaxMana => isConstructed 
            ? maxMana 
            : throw new UnassignedReferenceException("PlayerConfig isn't constructed before accessing it");
        public float MouseSensitivity => isConstructed 
            ? mouseSensitivity 
            : throw new UnassignedReferenceException("PlayerConfig isn't constructed before accessing it");

        public float HookDistance => isConstructed
            ? hookDistance
            : throw new UnassignedReferenceException("PlayerConfig isn't constructed before accessing it");

        [SerializeField] private float maxHP;
        [SerializeField] private float maxMana;
        [SerializeField] private float mouseSensitivity;
        [SerializeField] private float hookDistance;
    }
}
