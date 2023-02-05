using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DG.Tweening;
using Game.Scripts.Infrastructure;
using Game.Scripts.Map;
using Game.Scripts.UI;
using ModestTree;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.GamePlay.UI
{
    public class BuildingJoystickController : UIScreen
    {
        [Inject] private IMapController _mapController;
        [Inject] private IInputManager _inputManager;
        [SerializeField] private RectTransform root;
        [SerializeField] private Ease openEase;
        [SerializeField] private Ease closeEase;

        private Tween _openCloseTween;

        [SerializeField] private BuildingAreaAsset[] assets;
        [SerializeField] private BuildingJoystickCell cellPrefab;

        [SerializeField] private float dr = 10f;
        [SerializeField] private float impactRadius = 150f;
        [SerializeField] private float impactScale = 1.5f;
        private readonly List<BuildingJoystickCell> _cells = new List<BuildingJoystickCell>();

        [SerializeField] private RectTransform joystickPoint;

        [SerializeField] private float[] _angles;
        
        private void Start()
        {
            OnScreenCloseEnd.Subscribe(_ => Clear()).AddTo(this);

            _mapController.OnCellPointerDown.Subscribe(_ =>
            {
                root.transform.position = Input.mousePosition;
                // OpenScreen();
                
                Fill(_);
            }).AddTo(this);

            _inputManager.OnTouch.Subscribe(_ => OpenScreen()).AddTo(this);
            //
            _mapController.OnCellPointerUp.Subscribe(_ =>
            {
                foreach (var cell in _cells)
                {
                    if (cell.IsActive)
                    {
                        cell.OnChoose.Execute();
                        break;
                    }
                }

                CloseScreen();
            }).AddTo(this);
            //
            _mapController.OnCellPointerMove.Subscribe(_ =>
            {
            
                Vector2 mousePosition = Input.mousePosition;
                joystickPoint.transform.position = mousePosition;

                foreach (var buildingJoystickCell in _cells)
                {
                    buildingJoystickCell.Scale(impactScale, joystickPoint, impactRadius);
                }
            }).AddTo(this);
        }

        public void Fill(CellView cellView)
        {

            int count = assets.Length;
            _angles = new float[count];
            float localFillRect = 360f / count;
            for (int i = 0; i < assets.Length; i++)
            {
                int j = i;
                var cell = Instantiate(cellPrefab, root);
                cell.Fill(localFillRect * j, localFillRect - dr, 
                    assets[j].upgrades[0].icon, assets[j].regionName, assets[j].upgrades[0].price);
                _angles[i] = localFillRect * j;
                
                _cells.Add(cell);

                cell.OnChoose.Take(1).Subscribe(_ =>
                {
                    cellView.Build(assets[j], 0);
                }).AddTo(cell);
            }
        }

        [Button]
        public override void OpenScreen()
        {
            base.OpenScreen();
            DOTween.Kill(_openCloseTween);
            root.localScale = Vector3.zero;
            _openCloseTween = root.DOScale(1f, 0.5f).SetEase(openEase);
        }

        [Button]
        public override void CloseScreen()
        {
            DOTween.Kill(_openCloseTween);
            _openCloseTween =root.DOScale(0f, 0.5f).SetEase(openEase).OnComplete(() => 
                base.CloseScreen());
            
        }

        private void Clear()
        {
            for (var i = _cells.Count - 1; i >= 0; i--)
            {
                Destroy(_cells[i].gameObject);
            }
            
            _cells.Clear();
        }

        public override void Dispose()
        {
            base.Dispose();
            DOTween.Kill(_openCloseTween);
            Clear();
        }
    }
}