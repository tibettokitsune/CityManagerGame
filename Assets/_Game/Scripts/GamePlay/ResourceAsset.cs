using UnityEngine;
using UnityEngine.Serialization;

namespace _Game.Scripts.GamePlay
{
    [CreateAssetMenu(fileName = "Resource asset", menuName = "GameAsset/Resource")]
    public class ResourceAsset : ScriptableObject
    {
        public string resourceName;
        public Sprite icon;
        public Transform view;
    }
}