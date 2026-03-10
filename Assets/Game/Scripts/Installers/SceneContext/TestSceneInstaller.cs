using System;
using System.Collections.Generic;
using System.Linq;
using Game.Common.Interfaces;
using Game.Input;
using Game.Player;
using Game.Player.Camera;
using Game.Scripts.Player.StateMachine;
using Game.Scripts.Player.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class TestSceneInstaller : MonoInstaller
    {
        [Header("State Physics Configs")] 
        [SerializeField] private GroundedPhysicsConfig groundedConfig;
        [SerializeField] private AirbornePhysicsConfig airborneConfig;
        
        [Header("Camera Configs")]
        [SerializeField] private CameraSettings cameraConfig;
        
        [Header("\nInstaller Data")]
        [SerializeField] private PlayerConfigData playerConfig;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject cameraPrefab;
        [SerializeField] private Transform startPoint;

        public override void InstallBindings()
        {
            print("TestSceneInstaller is started");

            InstallInput();
            InstallPlayer();
            InstallCamera();
        }

        private void InstallInput()
        {
            Container.Bind<GameInput>().AsSingle().NonLazy();
            Container.Bind<IPlayerInput>().To<InputReader>().AsSingle().NonLazy();
        }

        private void InstallPlayer()
        {
            playerPrefab.transform.position = startPoint.position;

            //bind state configs
            Container.Bind<GroundedPhysicsConfig>().FromInstance(groundedConfig).AsSingle(); 
            Container.Bind<AirbornePhysicsConfig>().FromInstance(airborneConfig).AsSingle();
            
            Container.Bind<CameraSettings>().FromInstance(cameraConfig).AsSingle();
            
            //stateTransitionTable
            StateTransitionTable table = new();
            table.AddTransition<AirborneState, GroundedState>();
            table.AddTransition<GroundedState, AirborneState>();
            
            Container.Bind<StateTransitionTable>().FromInstance(table).AsSingle().NonLazy();
            
            //instantiate all player states in container
            Dictionary<Type, PlayerState> playerStates = new Dictionary<Type, PlayerState>();
            var states = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x => typeof(PlayerState).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);
            foreach (var state in states)
            {
                playerStates.Add(state, (PlayerState)Container.Instantiate(state));
            }   

            //install player state machine
            Container.Bind<Dictionary<Type, PlayerState>>().FromInstance(playerStates).AsSingle().NonLazy();
            Container.Bind<PlayerStateMachine>().AsSingle().WithArguments(playerStates[typeof(GroundedState)]).NonLazy();
            
            //install player itself
            Container.Bind<PlayerConfigData>().FromInstance(playerConfig).AsSingle().NonLazy();
            Container.Bind<PlayerController>().FromComponentInNewPrefab(playerPrefab).AsSingle().Lazy();
            
            //triggerHub && stateTransitionHandler
            Container.Bind<TriggerHub>().FromNew().AsSingle().NonLazy();
            Container.Bind<StateTransitionHandler>().FromNew().AsSingle().NonLazy();
        }

        private void InstallCamera()
        {
            Container.Bind<Camera>().FromComponentInNewPrefab(cameraPrefab).AsSingle().NonLazy();
        }
    }
}