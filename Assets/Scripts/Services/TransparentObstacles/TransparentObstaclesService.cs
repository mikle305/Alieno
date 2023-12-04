using System.Collections.Generic;
using Additional.Game;
using UnityEngine;

namespace Services.TransparentObstacles
{
    public class TransparentObstaclesService : MonoSingleton<TransparentObstaclesService>
    {
        [SerializeField] private LayerMask _obstacleLayer;
        [SerializeField] private Vector3 _boxHalfSize = new(4, 0.5f, 7);
        [SerializeField] private Vector3 _offset;

        private ObjectsProvider _objectsProvider;
        private readonly Collider[] _currentHits = new Collider[30];
        private HashSet<TransparentObstacle> _previousTransparents = new();


        private void Start()
        {
            _objectsProvider = ObjectsProvider.Instance;
        }

        private void FixedUpdate()
        {
            Camera mainCamera = _objectsProvider.MainCamera;
            GameObject character = _objectsProvider.Character;
            if (IsCameraOrPlayerDisabled(mainCamera, character))
                return;

            int raycastedCount = RaycastFromCameraNonAlloc(mainCamera, character);
            if (raycastedCount == 0)
            {
                SetAllPreviousOpaque();
                return;
            }

            HashSet<TransparentObstacle> currentTransparent = SetRaycastedTransparent(raycastedCount);
            SetOtherPreviousOpaque(currentTransparent);
        }

        private HashSet<TransparentObstacle> SetRaycastedTransparent(int raycastedCount)
        {
            var currentTransparents = new HashSet<TransparentObstacle>();
            for (var i = 0; i < raycastedCount; i++)
            {
                if (!_currentHits[i].gameObject.TryGetComponent(out TransparentObstacle obstacle))
                    continue;

                if (!_previousTransparents.Contains(obstacle))
                    SetTransparent(obstacle, true);

                currentTransparents.Add(obstacle);
            }

            return currentTransparents;
        }

        private void SetOtherPreviousOpaque(HashSet<TransparentObstacle> currentTransparents)
        {
            foreach (TransparentObstacle previousTransparent in _previousTransparents)
            {
                if (currentTransparents.Contains(previousTransparent))
                    continue;

                SetTransparent(previousTransparent, false);
            }

            _previousTransparents = currentTransparents;
        }

        private void SetAllPreviousOpaque()
        {
            foreach (TransparentObstacle obstacleRenderer in _previousTransparents)
                SetTransparent(obstacleRenderer, false);

            _previousTransparents.Clear();
        }

        private int RaycastFromCameraNonAlloc(Camera mainCamera, GameObject character)
        {
            Vector3 cameraPosition = mainCamera.transform.position;
            Vector3 characterPosition = character.transform.position;

            Vector3 boxCenter = Vector3.Lerp(cameraPosition, characterPosition, 0.5f) + _offset;
            Quaternion boxOrientation = Quaternion.LookRotation(cameraPosition - characterPosition, Vector3.up);
            
            return Physics.OverlapBoxNonAlloc(
                boxCenter,
                _boxHalfSize,
                _currentHits,
                orientation: boxOrientation,
                _obstacleLayer
                );
        }

        private static bool IsCameraOrPlayerDisabled(Camera mainCamera, GameObject character)
            => mainCamera == null || character == null || !character.activeSelf;

        private static void SetTransparent(TransparentObstacle obstacle, bool isTransparent)
            => obstacle.SetTransparent(isTransparent);
    }
}