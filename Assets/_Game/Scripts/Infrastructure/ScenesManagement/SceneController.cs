using _Game.Scripts.Infrastructure.Configs;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Infrastructure.SceneManagement
{
    public class SceneController : ISceneController, IInitializable
    {
        [Inject] private ScenesConfig _scenesConfig;
        public void LoadMenu()
        {
            SceneManager.LoadSceneAsync(_scenesConfig.menuSceneName, LoadSceneMode.Additive);
        }

        private void OnMenuLoaded(Scene arg0, LoadSceneMode arg1)
        {
            SceneManager.SetActiveScene(arg0);
        }

        public void Initialize()
        {
            SceneManager.sceneLoaded += OnMenuLoaded;
        }
    }
}