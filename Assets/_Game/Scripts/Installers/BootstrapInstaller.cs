using Game.Scripts.Infrastructure;

namespace Game.Infrastructure.Installers
{
    using SceneManagement;
    using Zenject;

    namespace _Game.Scripts.Installers
    {
        public class BootstrapInstaller : MonoInstaller
        {
            public override void InstallBindings()
            {
                Container.BindInterfacesTo<SceneController>().AsSingle();
                Container.BindInterfacesTo<BootstrapController>().AsSingle();
                Container.BindInterfacesTo<InputManager>().AsSingle();
            }
        }
    }
}