using Game.Common.Interfaces;
using Game.Input;
using Zenject;

namespace Game.Installers
{
    public class TestSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameInput>().AsSingle().NonLazy();
            Container.Bind<IPlayerInput>().To<InputReader>().AsSingle().NonLazy();
        }
    }
}