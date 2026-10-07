using System;
using System.Collections.Generic;
using System.Linq;
using Game.Input;
using Game.Player;
using Game.Player.Abilities;
using Game.Player.Camera;
using Game.Scripts;
using Game.Scripts.Player.Input.Common;
using Game.Scripts.Player.StateMachine;
using Game.Scripts.Player.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class TestSceneInstaller : MonoInstaller
    {
        [Header("\nInstaller Data")]
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform startPoint;

        
        public override void InstallBindings()
        {
            print("TestSceneInstaller is started");

            InstallInput();
            InstallPlayer();
        }

        private void InstallInput()
        {
            Container.Bind<GameInput>().AsSingle().NonLazy();
            Container.Bind<IPlayerInput>().To<InputReader>().AsSingle().NonLazy();
        }

        private void InstallPlayer()
        {
            Container.Bind<PlayerController>()
                .FromComponentInNewPrefab(playerPrefab)
                .WithGameObjectName("Player")
                .UnderTransform(startPoint)
                .AsSingle()
                .NonLazy();
        }
    }
}