#if UNITY_EDITOR || UNITY_EDITOR_OSX
using UnityEditor;
#endif
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "ScenesConfig", menuName = "Configs/Scenes")]
    public class ScenesConfig : ScriptableObject
    {
#if UNITY_EDITOR || UNITY_EDITOR_OSX
        [OnValueChanged("UpdateGameValues")]
        public SceneAsset menuScene;
        [OnValueChanged("UpdateGameValues")]
        public SceneAsset testGameScene;

        private void UpdateGameValues()
        {
            menuSceneName = menuScene.name;
            testGameSceneName = testGameScene.name;
        }
#endif

        [ReadOnly]
        public string menuSceneName;
        [ReadOnly]
        public string testGameSceneName;
    }
}