using Game.Infrastructure.SceneManagement;
using Zenject;

namespace Game.Scripts.Infrastructure
{
    public class BootstrapController : IInitializable
    {
        [Inject] private ISceneController _sceneController;

        public void Initialize()
        {
            _sceneController.LoadMenu();
        }
    }
}