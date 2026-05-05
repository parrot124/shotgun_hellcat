using System;
using UnityEngine;

namespace Game.Scripts.Player.Input.Common
{
    public interface IPlayerInput
    {
        public event Action<Vector2> MouseMove;
        public event Action<Vector2> Move;

        public event Action Jump;
        public event Action JumpCanceled;
        public event Action Sneak;
        public event Action Pause;
        public event Action Hook;
    }
}
