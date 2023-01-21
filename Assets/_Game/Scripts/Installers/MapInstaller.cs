using Game.Scripts.Map;
using Zenject;

namespace Game.Infrastructure.Installers
{
    public class MapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<MapController>().AsSingle();
            Container.BindFactory<UnityEngine.Object, CellView, CellView.Factory>()
                .FromFactory<PrefabFactory<CellView>>();
        }
    }
}