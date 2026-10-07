using System;
using System.Collections.Generic;
using System.Linq;
using Game.Player;
using Game.Player.Abilities;
using Game.Player.Camera;
using Game.Scripts.Game.Scripts.Player;
using Game.Scripts.Player.StateMachine;
using Game.Scripts.Player.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Core.Installers.GameObject_Installer
{
    public class PlayerInstaller : MonoInstaller
    {
        [Header("State Physics Configs")] 
        [SerializeField] private GroundedPhysicsConfig groundedConfig;
        [SerializeField] private AirbornePhysicsConfig airborneConfig;
        [SerializeField] private HookingStateConfig hookingConfig;
    
        [Header("Player Configs")]
        [SerializeField] private HookConfig hookConfig;
        [SerializeField] private PlayerConfigData playerConfig;
    
        [Header("Camera Configs")]
        [SerializeField] private CameraSettings cameraConfig;
        [SerializeField] private GameObject cameraPrefab;
        
        public override void InstallBindings()
        {
            //bind state configs
            Container.Bind<GroundedPhysicsConfig>().FromInstance(groundedConfig).AsSingle();
            Container.Bind<AirbornePhysicsConfig>().FromInstance(airborneConfig).AsSingle();
            Container.Bind<HookingStateConfig>().FromInstance(hookingConfig).AsSingle();

            Container.Bind<HookConfig>().FromInstance(hookConfig).AsSingle();
            Container.Bind<CameraSettings>().FromInstance(cameraConfig).AsSingle();

            //stateTransitionTable
            StateTransitionTable table = new();
            table.AddTransition<AirborneState, GroundedState>();
            table.AddTransition<GroundedState, AirborneState>();
            table.AddTransition<AirborneState, HookingState>();
            table.AddTransition<GroundedState, HookingState>();
            table.AddTransition<HookingState, HookingState>();

            Container.Bind<StateTransitionTable>().FromInstance(table).AsSingle().Lazy();

            //instantiate all player states in container
            Dictionary<Type, PlayerState> playerStates = new Dictionary<Type, PlayerState>();
            var states = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x =>
                typeof(PlayerState).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);
            foreach (var state in states)
            {
                playerStates.Add(state, (PlayerState)Container.Instantiate(state));
            }

            //install player state machine
            Container.Bind<Dictionary<Type, PlayerState>>().FromInstance(playerStates).AsSingle().Lazy();
            Container.Bind<PlayerStateMachine>().AsSingle().WithArguments(playerStates[typeof(GroundedState)]).Lazy();

            //install player itself
            Container.Bind<PlayerConfigData>().FromInstance(playerConfig).AsSingle().Lazy();
            Container.Bind<PlayerController>().FromComponentOnRoot().AsSingle().Lazy();

            //player observer
            Container.Bind<PlayerObserver>().FromComponentOnRoot().AsSingle().NonLazy();
        
            //triggerHub && stateTransitionHandler
            Container.Bind<TriggerHub>().FromNew().AsSingle().NonLazy();
            Container.Bind<StateTransitionHandler>().FromNew().AsSingle().NonLazy();
            
            Container.Bind<Camera>().FromComponentInNewPrefab(cameraPrefab).AsSingle().NonLazy();
        }
    }
}