using _Game.Scripts.Infrastructure.Configs;
using Zenject;

namespace Game.Infrastructure.Installers
{
    public class ConfigsInstaller : MonoInstaller
    {
        public ScenesConfig scenesConfig;
        public override void InstallBindings()
        {
            Container.BindInstance(scenesConfig);
        }
    }
}