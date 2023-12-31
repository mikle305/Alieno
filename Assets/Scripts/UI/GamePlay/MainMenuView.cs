﻿using System.Linq;
using System.Threading;
using DG.Tweening;
using Services;
using UnityEngine;

namespace UI.GamePlay
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Transform _mainCamera;
        [SerializeField] private Transform[] _cameraPath;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _uiFadeDuration = 0.5f;
        [SerializeField] private float _cameraMoveDuration = 1.5f;

        private MainMenuService _menuService;
        private Tween _sequenceTween;
        private CancellationTokenSource _rotateCameraTokenSource;


        private void Awake()
        {
            _menuService = MainMenuService.Instance;
            _menuService.PlayClicked += OnPlayClicked;
        }

        private void OnPlayClicked()
        {
            _menuService.PlayClicked -= OnPlayClicked;
            _rotateCameraTokenSource = new CancellationTokenSource();
            _sequenceTween = DOTween.Sequence()
                .Append(FadeUi())
                .Append(MoveAndRotateCameraByPath())
                .OnComplete(OnSequenceCompleted);
        }

        private void OnSequenceCompleted()
        {
            _rotateCameraTokenSource?.Cancel();
            _menuService.StartGame();
        }

        private Tween MoveAndRotateCameraByPath()
        {
            Vector3[] path = _cameraPath.Select(t => t.position).ToArray();
            return _mainCamera
                .DOPath(path, _cameraMoveDuration, PathType.CatmullRom, gizmoColor: Color.red)
                .OnUpdate(RotateCamera);
        }

        private void RotateCamera()
        {
            Transform target = _cameraPath[^1];
            Quaternion targetRotation = Quaternion.LookRotation(target.position - _mainCamera.position);
            _mainCamera.rotation = Quaternion.Slerp(_mainCamera.rotation, targetRotation, 5 * Time.deltaTime);
        }

        private void OnDestroy() 
            => _sequenceTween?.Kill();

        private Tween FadeUi() 
            => _canvasGroup.DOFade(0, _uiFadeDuration);
    }
}