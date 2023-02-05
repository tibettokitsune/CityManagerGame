using System;
using Game.Scripts.Infrastructure;
using Game.Scripts.Infrastructure.Configs;
using ModestTree;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.GamePlay.UI
{
    public class GameResourcesPanelController : MonoBehaviour
    {
        [Inject] private IPlayerDataController _dataController;
        [Inject] private GameItemsConfig _gameItemsConfig;
        [SerializeField] private Transform cellsRoot;
        [SerializeField] private ResourceUICell cellPrefab;

        private void Start()
        {
            CreateCells();
        }

        private void CreateCells()
        {
            foreach (var resourceAsset in _gameItemsConfig.resourceAssets)
            {
                var cell = Instantiate(cellPrefab, cellsRoot);
                cell.Fill(resourceAsset);
                
                var index = _gameItemsConfig.resourceAssets.IndexOf(resourceAsset);
                cell.UpdateText(_dataController.PlayerData.resources[index]);
                _dataController.OnResourceDataUpdate.Subscribe(_ =>
                {
                    cell.UpdateText(_dataController.PlayerData.resources[index]);
                }).AddTo(this);
            }
        }
    }
}