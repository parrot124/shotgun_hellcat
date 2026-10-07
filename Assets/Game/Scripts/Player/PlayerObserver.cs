using System;
using Game.Scripts.Core.Extensions.CharacterController;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Game.Scripts.Player
{
    /// <summary>
    /// Observes state-critical fields from PlayerController
    /// </summary>
    public class PlayerObserver : MonoBehaviour
    {
        private CharacterController subject;
        public event Action<bool> OnGroundedChange;

        //runtime fields
        private bool lastGrounded;        
        
        [Inject]
        private void Construct(PlayerController playerController)
        {
            subject = playerController.GetComponent<CharacterController>();
            lastGrounded = subject.IsGroundedSpherecast();
        }

        private void FixedUpdate()
        {
            GroundedCheck(subject);
        }

        private void GroundedCheck(CharacterController controller)
        {
            bool currentGrounded = controller.isGrounded;
            
            if (lastGrounded != currentGrounded) OnGroundedChange?.Invoke(currentGrounded);
            
            lastGrounded = currentGrounded;
        }
    }
}