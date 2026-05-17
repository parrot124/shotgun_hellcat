using UnityEngine;

namespace Game.Scripts
{
    //player context for Player FSM
    public class PlayerContext : MonoBehaviour
    {
        public CharacterController playerController => GetComponent<CharacterController>();
        public Transform playerTransform => playerController.transform;
    }
}
