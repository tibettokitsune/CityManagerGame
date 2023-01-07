using System.Collections.Generic;
using Game.Scripts.Infrastructure.Configs;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Infrastructure.SceneManagement
{
    public class SceneController : ISceneController, IInitializable
    {
        [Inject] private ScenesConfig _scenesConfig;
        private List<string> _currentScenes = new List<string>();
        public void LoadMenu()
        {
            UnloadAllScenes();
            SceneManager.LoadSceneAsync(_scenesConfig.menuSceneName, LoadSceneMode.Additive);
        }

        public void StartTestGame()
        {
            UnloadAllScenes();
            SceneManager.LoadSceneAsync(_scenesConfig.testGameSceneName, LoadSceneMode.Additive);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
        {
            SceneManager.SetActiveScene(scene);
            _currentScenes.Add(scene.name);
        }

        private void UnloadScene(string sceneName)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }

        private void UnloadAllScenes()
        {
            if(_currentScenes.Count <= 0) return;
            
            for (var i = _currentScenes.Count - 1; i >= 0; i--)
            {
                UnloadScene(_currentScenes[i]);
                _currentScenes.RemoveAt(i);
            }
        }

        public void Initialize()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }
}