using System;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace Game.Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIScreen : MonoBehaviour, IDisposable
    {
        protected  ReactiveCommand OnScreenOpenEnd { get; } = new ReactiveCommand();
        protected  ReactiveCommand OnScreenOpenStart { get; } = new ReactiveCommand();
        protected  ReactiveCommand OnScreenCloseEnd { get; } = new ReactiveCommand();
        protected  ReactiveCommand OnScreenCloseStart { get; } = new ReactiveCommand();
        [ReadOnly]
        private CanvasGroup _canvasGroup;

        public bool isBlockRaycast = true;
        private void OnValidate()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        //[Button]
        public virtual void OpenScreen()
        {
            OnScreenOpenStart.Execute();
            _canvasGroup.alpha = 1f;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = isBlockRaycast? true : false;
            OnScreenOpenEnd.Execute();
        }
        
        //[Button]
        public virtual void CloseScreen()
        {
            OnScreenCloseStart.Execute();
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            OnScreenCloseEnd.Execute();
        }

        public virtual void Dispose()
        {
            OnScreenOpenEnd?.Dispose();
            OnScreenOpenStart?.Dispose();
            OnScreenCloseEnd?.Dispose();
            OnScreenCloseStart?.Dispose();
        }
    }
}