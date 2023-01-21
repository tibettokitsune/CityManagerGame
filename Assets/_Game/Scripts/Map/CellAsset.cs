using _Game.Scripts.GamePlay;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Map
{
    [CreateAssetMenu(fileName = "CellAsset", menuName = "Configs/CellAsset")]
    public class CellAsset : ScriptableObject
    {
        public CellView viewCellPrefab;
    }

    
    [System.Serializable]
    public class HexCell
    {
        public HexCoordinates hexCoordinates;
        public RealCoordinates realCoordinates;
        public HexCell(int x, int z, float y)
        {
            hexCoordinates = new HexCoordinates(x, z, y);
            realCoordinates = new RealCoordinates(hexCoordinates);
        }

        public BuildingAreaAsset buildingAreaAsset;
        public int buildingAreaLevel;
    }
    
    [System.Serializable]
    public struct HexCoordinates
    {

        [SerializeField, ReadOnly] private int x;
        [SerializeField, ReadOnly] private int z;
        [SerializeField, ReadOnly] private float y;

        public int X { get; private set; }
        public int Z { get; private set; }
        
        public float Y { get; private set; }

        public HexCoordinates (int x, int z, float y) {
            X = x;
            Z = z;
            Y = y;
            
            this.x = x;
            this.z = z;
            this.y = y;
        }
    }

    [System.Serializable]
    public struct RealCoordinates
    {

        [SerializeField, ReadOnly] private Vector3 position;

        public Vector3 Position { get; private set; }
        

        public RealCoordinates (HexCoordinates hexCoordinates)
        {
            Position = new Vector3(
                (hexCoordinates.X + hexCoordinates.Z * 0.5f - hexCoordinates.Z / 2) * (HexMetrics.innerRadius * 2f), 
                0f,//hexCoordinates.Y,
                hexCoordinates.Z * (HexMetrics.outerRadius * 1.5f));
            position = Position;
        }
    }
}