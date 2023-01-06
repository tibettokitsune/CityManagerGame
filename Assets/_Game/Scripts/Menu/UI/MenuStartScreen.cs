using System;
using _Game.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Menu.UI
{
    public class MenuStartScreen : UIScreen
    {
        [SerializeField] private Button startGameBtn;
        [SerializeField] private Button quitGameBtn;

        private void Start()
        {
            quitGameBtn.onClick.AddListener(() => { Application.Quit();});
        }
        
        
    }
}