using _Game.Scripts.GamePlay;
using Game.Scripts.Infrastructure.Configs;
using ModestTree;
using UniRx;
using Zenject;

namespace Game.Scripts.Infrastructure
{
    [System.Serializable]
    public class PlayerData
    {
        public int id;
        public int[] resources;
    }

    public interface IPlayerDataController
    {
        PlayerData PlayerData { get; }
        ReactiveCommand OnResourceDataUpdate { get; }
        bool IncrementResource(int value, ResourceAsset resourceAsset);
    }
    
    public class PlayerDataController : IInitializable, IPlayerDataController
    {
        [Inject] private GameItemsConfig _itemsConfig;
        public PlayerData PlayerData { get; private set; }
        public ReactiveCommand OnResourceDataUpdate { get; } = new ReactiveCommand();
        
        public void Initialize()
        {
            PlayerData = new PlayerData()
            {
                resources = new int[_itemsConfig.resourceAssets.Length]
            };
        }

        public bool IncrementResource(int value, ResourceAsset resourceAsset)
        {
            var index = _itemsConfig.resourceAssets.IndexOf(resourceAsset);

            var newValue = PlayerData.resources[index] + value;
            if (newValue >= 0)
            {
                PlayerData.resources[index] = newValue;
                return true;
            }

            return false;
        }
    }
}