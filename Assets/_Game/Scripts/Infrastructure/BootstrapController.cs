using Game.Infrastructure.SceneManagement;
using Zenject;

namespace _Game.Scripts.Infrastructure
{
    public interface IBootstrapController
    {
        public void StartMenu();
    }
    public class BootstrapController : IBootstrapController, IInitializable
    {
        [Inject] private ISceneController _sceneController;
        public void StartMenu()
        {
            _sceneController.LoadMenu();
        }

        public void Initialize()
        {
            StartMenu();
        }
    }
}