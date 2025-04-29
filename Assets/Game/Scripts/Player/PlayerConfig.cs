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

            speed = data.DefaultSpeed;
            maxHP = data.MaxHP;
            maxMana = data.MaxMana;
            mouseSensitivity = data.MouseSensitivity;
            jumpForce = data.JumpForce;
            gravityMultiplier = data.GravityMultiplier;
            dashForce = data.DashForce;
            dashCooldown = data.DashCooldown;

            isConstructed = true;
        }


        public float Speed => isConstructed ? speed : throw new UnassignedReferenceException("PlayerConfig isn't constructed before accessing it");
        public float MaxHP => isConstructed ? maxHP : throw new UnassignedReferenceException("PlayerConfig isn't constructed before accessing it");
        public float MaxMana => isConstructed ? maxMana : throw new UnassignedReferenceException("PlayerConfig isn't constructed before accessing it");
        public float MouseSensitivity => isConstructed ? mouseSensitivity : throw new UnassignedReferenceException("PlayerConfig isn't constructed before accessing it");
        public float JumpForce => isConstructed ? jumpForce : throw new UnassignedReferenceException("PlayerConfig isn't constructed before accessing it");
        public float GravityMultiplier => isConstructed ? gravityMultiplier : throw new UnassignedReferenceException("PlayerConfig isn't constructed before accessing it");
        public float DashForce => isConstructed ? dashForce : throw new UnassignedReferenceException("PlayerConfig isn't constructed before accessing it");
        public float DashCooldown => isConstructed ? dashCooldown : throw new UnassignedReferenceException("PlayerConfig isn't constructed before accessing it");
        

        [SerializeField] private float speed;
        [SerializeField] private float maxHP;
        [SerializeField] private float maxMana;
        [SerializeField] private float mouseSensitivity;
        [SerializeField] private float jumpForce;
        [SerializeField] private float gravityMultiplier;
        [SerializeField] private float dashForce;
        [SerializeField] private float dashCooldown;
    }
}
