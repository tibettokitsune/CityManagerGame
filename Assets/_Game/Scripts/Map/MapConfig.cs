using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Map
{
    [CreateAssetMenu(fileName = "MapConfig", menuName = "Configs/Map")]
    public class MapConfig : ScriptableObject
    {
        public CellData[] cells;

        public MapConfig()
        {
            var cellTypes = System.Enum.GetValues(typeof(LandscapeType));

            cells = new CellData[cellTypes.Length];

            var i = 0;

            foreach (var cellType in cellTypes)
            {
                cells[i++] = new CellData()
                {
                    landscapeType = (LandscapeType) cellType
                };
            }
        }
    }

    [Serializable]
    public class CellData
    {
        public CellAsset viewAsset;
        [ReadOnly] public LandscapeType landscapeType;
        public Color color;
    }
    
    public enum LandscapeType
    {
        Water, 
        Sand, 
        Planes, 
        Wood, 
        Rocks, 
        Mountains
    }
}