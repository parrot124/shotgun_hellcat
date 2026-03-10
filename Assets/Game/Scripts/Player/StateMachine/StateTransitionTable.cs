using System;
using System.Collections.Generic;

namespace Game.Scripts.Player.StateMachine
{
    public class StateTransitionTable
    {
        private Dictionary<Type, HashSet<Type>> allowedTransitions = new();

        public void AddTransition<TTo, TFrom>() where TTo : IPlayerState where TFrom : IPlayerState
        {
            Type from = typeof(TFrom);

            if (allowedTransitions.ContainsKey(from) == false)
            {
                allowedTransitions.Add(from, new HashSet<Type>());
            }
        
            allowedTransitions[from].Add(typeof(TTo));
        }

        public bool TransitionAllowed(Type current, Type next)
        {
            return allowedTransitions.TryGetValue(current, out var set) && set.Contains(next);
        }
    }
}