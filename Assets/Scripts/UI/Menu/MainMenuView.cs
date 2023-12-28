using System.Threading;
using DG.Tweening;
using PathCreation;
using Services;
using UnityEngine;
using VContainer;

namespace UI.Menu
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Transform _mainCamera;
        [SerializeField] private PathCreator _cameraPath;
        [SerializeField] private Transform _cameraLookAt;
        [SerializeField] private CanvasGroup[] _canvasGroups;
        
        [SerializeField] private float _cameraMoveDuration = 1.5f;
        [SerializeField] private float _cameraRotateSpeed = 2;
        [SerializeField] private float _uiFadeDuration = 0.5f;

        private MainMenuService _menuService;
        private Tween _sequenceTween;
        private CancellationTokenSource _rotateCameraTokenSource;


        [Inject]
        public void Construct(MainMenuService menuService)
        {
            _menuService = menuService;
            _menuService.PlayClicked += OnPlayClicked;
        }

        private void OnDestroy()
        {
            _menuService.PlayClicked -= OnPlayClicked;
            _sequenceTween?.Kill();
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
            float timer = 0;
            return DOTween
                .To(getter: () => timer, setter: x => timer = x,
                    endValue: _cameraMoveDuration, _cameraMoveDuration)
                .OnUpdate(() => MoveAndRotateCamera(timer, total: _cameraMoveDuration));
        }

        private void MoveAndRotateCamera(float timer, float total)
        {
            float timeParam = timer / total;
            if (timeParam >= 1)
                return;
            
            MoveCamera(timeParam);
            RotateCamera();
        }

        private void MoveCamera(float timeParam)
        {
            _mainCamera.position = _cameraPath.path.GetPointAtTime(timeParam);
        }

        private void RotateCamera()
        {
            Quaternion targetRotation = Quaternion.LookRotation(_cameraLookAt.position - _mainCamera.position);
            _mainCamera.rotation = Quaternion.Slerp(_mainCamera.rotation, targetRotation, _cameraRotateSpeed * Time.deltaTime);
        }

        private Tween FadeUi()
        {
            Sequence sequence = DOTween.Sequence();
            foreach (CanvasGroup canvasGroup in _canvasGroups)
            {
                canvasGroup.interactable = false;
                sequence.Join(canvasGroup.DOFade(0, _uiFadeDuration));
            }

            return sequence;
        }
    }
}