using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Map
{

    public interface IMapController
    {
        public ReactiveCommand<CellView> OnCellPointerDown { get; }
        public ReactiveCommand<CellView> OnCellPointerUp { get; }
        public ReactiveCommand<CellView> OnCellPointerMove { get; }

        public void CreateCell(HexCell cell);
    }
    public class MapController : IMapController, IInitializable
    {
        [Inject] private MapConfig _mapConfig;
        public ReactiveCommand<CellView> OnCellPointerDown { get; } = new ReactiveCommand<CellView>();
        public ReactiveCommand<CellView> OnCellPointerUp { get; } = new ReactiveCommand<CellView>();
        public ReactiveCommand<CellView> OnCellPointerMove { get; } = new ReactiveCommand<CellView>();

        private readonly CellView.Factory _cellFactory;

        private Transform _cellsTransform;
        
        public MapController(CellView.Factory cellFactory)
        {
            _cellFactory = cellFactory;
        }

        public void CreateCell(HexCell cell)
        {
            var cellPrefab =_cellFactory.Create(
                _mapConfig.cells[(int)(cell.hexCoordinates.Y * _mapConfig.cells.Length)].viewAsset.viewCellPrefab);
            var transform = cellPrefab.transform;
            transform.position = cell.realCoordinates.Position;
            transform.parent = _cellsTransform;
            cellPrefab.HexCell = cell;
        }

        public void Initialize()
        {
            _cellsTransform = new GameObject("Cells root").transform;
        }
    }
}