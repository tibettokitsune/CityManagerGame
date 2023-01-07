using System;
using Game.Infrastructure.SceneManagement;
using Game.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.Menu.UI
{
    public class MenuStartScreen : UIScreen
    {
        [Inject] private ISceneController _sceneController;
        [SerializeField] private Button startGameBtn;
        [SerializeField] private Button quitGameBtn;

        private void Start()
        {
            quitGameBtn.onClick.AddListener(() => { Application.Quit();});
            startGameBtn.onClick.AddListener(() => _sceneController.StartTestGame());
        }
        
        
    }
}