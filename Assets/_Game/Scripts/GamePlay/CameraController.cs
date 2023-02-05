using System;
using Cinemachine;
using Game.Scripts.Infrastructure;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.GamePlay
{
    public class CameraController : MonoBehaviour
    {
        [Inject] private IInputManager _inputManager;
        [SerializeField] private CinemachineVirtualCamera cmCamera;
        [SerializeField] private float sensitivity;

        private Vector3 _startCameraPosiiton;
        private void Start()
        {
            _inputManager.OnSwipeStart.Subscribe(_=> StartCameraMovement()).AddTo(this);
            _inputManager.OnSwipe.Subscribe(MoveCamera).AddTo(this);
            _inputManager.OnSwipeEnd.Subscribe(_ => EndCameraMovement()).AddTo(this);
        }

        private void StartCameraMovement()
        {
            _startCameraPosiiton = cmCamera.transform.position;
        }
        private void MoveCamera(Vector2 moveVector)
        {
            cmCamera.transform.position =
                _startCameraPosiiton + new Vector3(moveVector.x, 0f, moveVector.y) * sensitivity;
        }

        private void EndCameraMovement()
        {
            
        }
    }
}