using Game.Scripts.Infrastructure;
using Zenject;

namespace Game.Infrastructure.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<PlayerDataController>().AsSingle();
        }
    }
}