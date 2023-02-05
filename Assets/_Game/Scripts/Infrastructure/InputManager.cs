using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Infrastructure
{
    public interface IInputManager
    {
        ReactiveCommand OnTouch { get; }
        ReactiveCommand OnSwipeStart { get; }
        ReactiveCommand OnSwipeEnd { get; }

        ReactiveCommand<Vector2> OnSwipe { get; }
    }
    
    public class InputManager : IInputManager, ITickable
    {
        //TODO: touch, swipe
        public ReactiveCommand OnTouch { get; } = new ReactiveCommand();
        public ReactiveCommand OnSwipeStart { get; } = new ReactiveCommand();
        public ReactiveCommand OnSwipeEnd { get; } = new ReactiveCommand();
        public ReactiveCommand<Vector2> OnSwipe { get; } = new ReactiveCommand<Vector2>();

        private const float TouchDelay = 0.3f;
        private bool _isDetection;
        private bool _isTouch;
        private bool _isSwipe;
        private Vector2 _swipeMousePosition;
        private float _startTouchTime;
        
        public void Tick()
        {
            if (Input.GetMouseButton(0))
            {
                if (!_isDetection)
                {
                    _isDetection = true;
                    _startTouchTime = Time.time;
                    _swipeMousePosition = Input.mousePosition;
                    _isTouch = false;
                    _isSwipe = false;
                }
                else
                {
                    if (Time.time - _startTouchTime > TouchDelay && !_isTouch && !_isSwipe)
                    {
                        OnTouch.Execute();
                        _isTouch = true;
                    }

                    if ((_swipeMousePosition - (Vector2)Input.mousePosition).magnitude > 0 && !_isSwipe && !_isTouch)
                    {
                        _isSwipe = true;
                        OnSwipeStart.Execute();
                    }

                    if (_isSwipe)
                    {
                        OnSwipe.Execute(_swipeMousePosition - (Vector2) Input.mousePosition);
                    }
                }
            }
            else
            {
                _isDetection = false;
                _isTouch = false;
                if (_isSwipe)
                {
                    OnSwipeEnd.Execute();
                }
                _isSwipe = false;
                
            }
        }
    }
}