using System;
using UnityEngine;

namespace Game.Common.Interfaces
{
    public interface IPlayerInput
    {
        public event Action<Vector2> MouseMove;
        public event Action<Vector2> Move;

        public event Action Jump;
        public event Action Sneak;
        public event Action Pause;
    }
}
