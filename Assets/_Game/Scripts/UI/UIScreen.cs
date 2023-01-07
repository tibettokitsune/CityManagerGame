using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIScreen : MonoBehaviour
    {
        [ReadOnly]
        private CanvasGroup _canvasGroup;

        private void OnValidate()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        [Button]
        public void OpenScreen()
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }
        
        [Button]
        public void CloseScreen()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}