using System.Collections.Generic;
using Additional.Game;
using TriInspector;
using UnityEngine;

namespace Services
{
    public class RadarService : MonoSingleton<RadarService>
    {
        [SerializeField] private Transform _player;
        [SerializeField] private List<Transform> _aliveEnemies;

        [ShowInInspector] private Transform _currentClosest;
        [ShowInInspector] private Transform _currentClosestAndVisible;
    
        private int _lastUsedFrame =-1;
        private List<Transform> _sortedEnemies;
    

        private void Start()
        {
            _player = ObjectsProvider.Instance.Character.transform;
            _aliveEnemies = ObjectsProvider.Instance.AliveEnemies;
        }

        public Transform GetClosestAndVisibleEnemy()
        {
            if (_lastUsedFrame == Time.frameCount || _aliveEnemies.Count == 0)
                return _currentClosestAndVisible;

            _lastUsedFrame = Time.frameCount;
            _sortedEnemies = new List<Transform>(_aliveEnemies);
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
            if (Physics.Linecast(_player.position, enemy.position))
            {
                return false;
            }
            else
            {
                Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
                Gizmos.DrawLine(_player.position, enemy.position);
                return true;
            }
        }
        
        private int CompareDistances(Transform prevEnemy, Transform nextEnemy)
        {
            var playerPos = _player.position;
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
