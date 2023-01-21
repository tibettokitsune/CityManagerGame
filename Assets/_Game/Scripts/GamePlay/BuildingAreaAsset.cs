using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.GamePlay
{
    public enum BuildingAreaType
    {
        TownArea,
        MilitaryArea,
        TradingArea,
        AgriculturalArea,
        IndustrialArea
    }
    
    [CreateAssetMenu(fileName = "Building region asset", menuName = "GameAsset/BuildingRegion")]
    public class BuildingAreaAsset : ScriptableObject
    {
        public string regionName;
        public BuildingAreaType areaType;

        public BuildingAreaUpgrade[] upgrades;
    }

    [System.Serializable]
    public struct BuildingAreaUpgrade
    {
        public Sprite icon;
        public Price price;
        public Transform cellView;

        public Vector3 offset;
        public Vector3 euler;
        public Vector3 scale;
    }


    [System.Serializable]
    public struct Price
    {
        public ResourceAsset resourceAsset;
        [Min(0)]public int number;
    }
}