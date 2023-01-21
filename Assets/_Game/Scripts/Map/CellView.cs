using System;
using _Game.Scripts.GamePlay;
using Sirenix.OdinInspector;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Game.Scripts.Map
{
    public class CellView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Inject] private IMapController _mapController;
        [SerializeField, ReadOnly] public HexCell HexCell;

        private bool _isActive = false;

        private ReactiveCommand OnUpdate { get; } = new ReactiveCommand();
        private CompositeDisposable _disposable = new CompositeDisposable();

        private MeshCollider _collider;

        private Transform _currentBuildingView;
        private void Start()
        {
            _collider = gameObject.AddComponent<MeshCollider>();

        }

        public override string ToString()=>  HexCell.hexCoordinates.X + " " 
                                                                      + HexCell.hexCoordinates.Y + " " 
                                                                      + HexCell.hexCoordinates.Z;

        public void OnPointerUp(PointerEventData eventData)
        {
            _mapController.OnCellPointerUp.Execute(this);
            
            _disposable.Clear();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _mapController.OnCellPointerDown.Execute(this);
            OnUpdate.Subscribe(_ => _mapController.OnCellPointerMove.Execute(this)).AddTo(_disposable);
        }

        private void Update()
        {
            OnUpdate?.Execute();
        }

        public class Factory : PlaceholderFactory<UnityEngine.Object, CellView>
        {
            
            DiContainer _container;

            public Factory(DiContainer container)
            {
                _container = container;
            }

            public CellView Create(Transform prefab)
            {
                return _container.InstantiatePrefabForComponent<CellView>(prefab);
            }
        }

        public void Build(BuildingAreaAsset asset, int level)
        {
            HexCell.buildingAreaAsset = asset;
            HexCell.buildingAreaLevel = level;

            CreateBuildingView();
        }

        private void CreateBuildingView()
        {
            if(_currentBuildingView) Destroy(_currentBuildingView.gameObject);
            var currentUpgrade = HexCell.buildingAreaAsset.upgrades[HexCell.buildingAreaLevel];
            _currentBuildingView = Instantiate(currentUpgrade.cellView, transform);
            var cellTransform = _currentBuildingView.transform;
            cellTransform.localPosition = currentUpgrade.offset;
            cellTransform.localEulerAngles = currentUpgrade.euler;
            cellTransform.localScale = currentUpgrade.scale;

        }
    }
}