using System;
using System.Collections.Generic;
using Game.Scripts.Player.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Player.StateMachine
{
    public class PlayerStateMachine
    {
        public Type CurrentStateType => currentState.GetType();
    
        private PlayerState currentState;
        private StateTransitionTable stateTransitionTable;
    
        private Dictionary<Type, PlayerState> states = new();
        private Vector3 lastVelocity;
    
        [Inject(Id = "StartState")]
        public PlayerStateMachine(PlayerState startState, Dictionary<Type, PlayerState> states, StateTransitionTable stateTransitionTable)
        {
            currentState = startState;
            currentState.Enter();
        
            this.states = states;
            this.stateTransitionTable = stateTransitionTable;
        }
        
        public void Tick(ref Vector3 velocity, Vector3 wishDirection)
        {
            currentState.Update(ref velocity, wishDirection);
            lastVelocity = velocity;
        }

        public void CameraTick(ref Transform cameraTransform)
        {
            currentState.UpdateCamera(ref cameraTransform, lastVelocity);
        }

        public void SetState<T>() where T : PlayerState
        {
            Type nextState = typeof(T);

            if (nextState == CurrentStateType) return;
            if (!stateTransitionTable.TransitionAllowed(CurrentStateType, nextState)) return;
            
            EnterIn<T>();
        }

        public bool IsInState<T>() where T : PlayerState
        {
            return currentState is T;
        }

        public T GetState<T>() where T : PlayerState
        {
            return (T)currentState;
        }

        public void TryJump(ref Vector3 velocity)
        {
            if (currentState is AirborneState airborne)
            {
                airborne.TryAirJump(ref velocity);
            }

            if (currentState is GroundedState grounded)
            {
                grounded.TryJump(ref velocity);
                SetState<AirborneState>();
            }
        }
        
        private void EnterIn<T>() where T : PlayerState
        {
            currentState.Exit();
            currentState = states[typeof(T)];
            currentState.Enter();
        }

        public void TryHook(RaycastHit hitInfo)
        {
            //apply velocity towards hitInfo hit point
        }
    }
}