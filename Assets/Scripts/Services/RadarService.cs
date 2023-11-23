using System.Collections.Generic;
using Additional.Game;
using TriInspector;
using UnityEngine;

namespace Services
{
    public class RadarService : MonoSingleton<RadarService>
    {
        [ShowInInspector] private Transform _currentClosest;
        [ShowInInspector] private Transform _currentClosestAndVisible;
    
        private int _lastUsedFrame =-1;
        private List<Transform> _sortedEnemies;
        private ObjectsProvider _objectsProvider;


        private void Start()
        {
            _objectsProvider = ObjectsProvider.Instance;
        }

        public Transform GetClosestAndVisibleEnemy()
        {
            List<Transform> aliveEnemies = _objectsProvider.AliveEnemies;
            if (_lastUsedFrame == Time.frameCount || aliveEnemies.Count == 0)
                return _currentClosestAndVisible;

            _lastUsedFrame = Time.frameCount;
            _sortedEnemies = new List<Transform>(aliveEnemies);
            _sortedEnemies.Sort(CompareDistances);
            _currentClosest = _sortedEnemies[0];
            _currentClosestAndVisible = null;
            foreach (var enemy in _sortedEnemies)
            {
                if (IsVisible(enemy))
                {
                    _currentClosestAndVisible = enemy;
                    break;
                }
            }
        
        
            return _currentClosestAndVisible;
        }

        private bool IsVisible(Transform enemy)
        {
            Transform character = _objectsProvider.Character.transform;
            if (Physics.Linecast(character.position, enemy.position))
            {
                return false;
            }
            else
            {
                /*Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
                Gizmos.DrawLine(character.position, enemy.position);*/
                return true;
            }
        }
        
        private int CompareDistances(Transform prevEnemy, Transform nextEnemy)
        {
            Transform character = _objectsProvider.Character.transform;
            var playerPos = character.position;
            var prevDistance = (playerPos - prevEnemy.position).sqrMagnitude;
            var nextDistance = (playerPos - nextEnemy.position).sqrMagnitude;

            if (nextDistance > prevDistance)
                return 1;
        
            if (nextDistance < prevDistance)
                return -1;
        
            return 0;
        }
    }
}
