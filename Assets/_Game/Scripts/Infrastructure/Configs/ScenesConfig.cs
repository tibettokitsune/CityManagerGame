#if UNITY_EDITOR || UNITY_EDITOR_OSX
using UnityEditor;
#endif
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Scripts.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "ScenesConfig", menuName = "Configs/Scenes")]
    public class ScenesConfig : ScriptableObject
    {
#if UNITY_EDITOR || UNITY_EDITOR_OSX
        [OnValueChanged("UpdateGameValues")]
        public SceneAsset menuScene;

        private void UpdateGameValues()
        {
            menuSceneName = menuScene.name;
        }
#endif

        public string menuSceneName;
    }
}