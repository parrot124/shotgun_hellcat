using Game.Common.Interfaces;
using Game.Input;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class TestSceneInstaller : MonoInstaller
    {
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

            Container.Bind<PlayerController>().FromComponentInNewPrefab(playerPrefab).AsSingle().Lazy();
        }

        private void InstallCamera()
        {
            Container.Bind<Camera>().FromComponentInNewPrefab(cameraPrefab).AsSingle().NonLazy();
        }
    }
}