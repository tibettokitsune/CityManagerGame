using Game.Scripts.Infrastructure.Configs;
using Game.Scripts.Map;
using Zenject;

namespace Game.Infrastructure.Installers
{
    public class ConfigsInstaller : MonoInstaller
    {
        public ScenesConfig scenesConfig;
        public MapConfig mapConfig;
        public override void InstallBindings()
        {
            Container.BindInstance(scenesConfig);
            Container.BindInstance(mapConfig);
        }
    }
}