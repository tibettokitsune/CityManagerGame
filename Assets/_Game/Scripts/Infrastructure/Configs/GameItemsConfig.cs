using _Game.Scripts.GamePlay;
using UnityEngine;

namespace Game.Scripts.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "GameItems", menuName = "Configs/GameItemsConfig")]
    public class GameItemsConfig : ScriptableObject
    {
        public ResourceAsset[] resourceAssets;

        public BuildingAreaAsset[] buildingAreaAssets;
    }
}