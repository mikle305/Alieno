using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Services
{
    public class RadarService
    {
        private int _lastUsedFrame = -1;
        private int _lastUsedFrameEnemies = -1;
        private Transform _currentClosestAndVisible;
        private List<Transform> _sortedEnemies;
        private Dictionary<Transform, Transform> _enemyAndItsClosest;
        
        private readonly ObjectsProvider _objectsProvider;
        private readonly LayerMask _obstacleLayer;

        
        public RadarService(ObjectsProvider objectsProvider, StaticDataService staticDataService)
        {
            _objectsProvider = objectsProvider;
            _obstacleLayer = staticDataService.GetGamePlayConfig().ObstacleLayer;
        }

        public Transform GetClosestFromEnemy(Transform fromEnemy)
        {
            if (_lastUsedFrameEnemies == Time.frameCount && _objectsProvider.AliveEnemies.Count > 1)
            {
                _enemyAndItsClosest.TryGetValue(fromEnemy, out Transform toEnemy);
                if (toEnemy != null)
                    return toEnemy;
                
                return FindClosestToEnemy(fromEnemy);
            }

            if (_objectsProvider.AliveEnemies.Count <= 1) 
                return null;
            
            _enemyAndItsClosest = new Dictionary<Transform, Transform>();
            _lastUsedFrameEnemies = Time.frameCount;
            return FindClosestToEnemy(fromEnemy);
        }

        public Transform GetClosestFromPlayer()
        {
            List<Transform> aliveEnemies = _objectsProvider.AliveEnemies;
            if (_lastUsedFrame == Time.frameCount || aliveEnemies.Count == 0)
                return _currentClosestAndVisible;

            _lastUsedFrame = Time.frameCount;

            GameObject character = _objectsProvider.Character;
            if (character == null)
                return null;

            SortEnemiesFromCharacter(aliveEnemies, character);
            SetClosestVisibleFromCharacter(character);

            return _currentClosestAndVisible;
        }

        private Transform FindClosestToEnemy(Transform fromEnemy)
        {
            List<Transform> sortedEnemies = SortedEnemiesFromOther(_objectsProvider.AliveEnemies, fromEnemy);

            foreach (Transform enemy in sortedEnemies)
            {
                if (IsVisible(fromEnemy, enemy))
                {
                    _enemyAndItsClosest.Add(fromEnemy, enemy);
                    return enemy;
                }
            }

            return null;
        }

        private static int CompareDistances(Transform prev, Transform next, Vector3 fromPosition)
        {
            float prevDistance = (fromPosition - prev.position).sqrMagnitude;
            float nextDistance = (fromPosition - next.position).sqrMagnitude;

            if (nextDistance > prevDistance)
                return -1;

            if (nextDistance < prevDistance)
                return 1;

            return 0;
        }

        private void SortEnemiesFromCharacter(List<Transform> aliveEnemies, GameObject character)
        {
            _sortedEnemies = new List<Transform>(aliveEnemies);
            _sortedEnemies.Sort(comparison: (prev, next) 
                => CompareDistances(prev, next, character.transform.position));
            
            _currentClosestAndVisible = null;
        }

        private List<Transform> SortedEnemiesFromOther(List<Transform> aliveEnemies, Transform fromEnemy)
        {
            var sortedEnemies = new List<Transform>(aliveEnemies);
            sortedEnemies.Remove(fromEnemy);
            sortedEnemies.Sort((prev, next) 
                => CompareDistances(prev, next, fromEnemy.position));
            
            return sortedEnemies;
        }

        private void SetClosestVisibleFromCharacter(GameObject character)
        {
            foreach (Transform enemy in _sortedEnemies)
            {
                if (IsVisible(character.transform, enemy))
                {
                    _currentClosestAndVisible = enemy;
                    break;
                }
            }
        }

        private bool IsVisible(Transform from, Transform target)
            => !Physics.Linecast(
                from.position,
                target.position,
                out RaycastHit _,
                _obstacleLayer,
                QueryTriggerInteraction.UseGlobal);
    }
}