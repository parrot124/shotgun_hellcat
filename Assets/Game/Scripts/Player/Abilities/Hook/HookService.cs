using Game.Player.Abilities;
using Game.Scripts.Player.StateMachine;
using UnityEngine;

namespace Game.Scripts
{
    public class HookService : MonoBehaviour
    {
        private bool isHooking = false;
        
        public void ApplyHookPull(ref Vector3 velocity, HookConfig hookConfig, Vector3 wishDir, Vector3 hookPoint)
        {
            PlayerStateHelper.ApplyHookPull(ref velocity, hookPoint, this.transform, hookConfig, ref isHooking);
        }
    }
}
