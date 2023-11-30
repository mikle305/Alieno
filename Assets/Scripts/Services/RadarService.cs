using System.Collections.Generic;
using Additional.Game;
using TriInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Services
{
    public class RadarService : MonoSingleton<RadarService>
    {
        [FormerlySerializedAs("_layersToIgnore")] [SerializeField] private LayerMask _layerToCheckFor;
        [ShowInInspector] private Transform _currentClosest;
        [ShowInInspector] private Transform _currentClosestAndVisible;
    
        private int _lastUsedFrame =-1;
        private int _lastUsedFrameEnemies =-1;
        
        private List<Transform> _sortedEnemies;
        private ObjectsProvider _objectsProvider;

        private Transform _ricoshetFrom;
        private Dictionary<Transform, Transform> _enemyAndItsClosest;

        private void Start()
        {
            _objectsProvider = ObjectsProvider.Instance;
        }

        public Transform GetClosestToEnemy(Transform ricoshetFrom)
        {
            Transform ricoshetTo = null;
            _ricoshetFrom = ricoshetFrom;
            
            if (_lastUsedFrameEnemies == Time.frameCount && _objectsProvider.AliveEnemies.Count > 1)
            {
                _enemyAndItsClosest.TryGetValue(ricoshetFrom, out ricoshetTo);
                if (ricoshetTo == null)
                {
                    ricoshetTo = FindClosestToEnemy();
                }
            }
            else if (_objectsProvider.AliveEnemies.Count > 1)
            {
                _enemyAndItsClosest = new Dictionary<Transform, Transform>();
                _lastUsedFrameEnemies = Time.frameCount;
                ricoshetTo = FindClosestToEnemy();
            }

            return ricoshetTo;
        }

        private Transform FindClosestToEnemy()
        {
            Transform ricoshetTo = null;
            
            List<Transform> sortedEnemiesRico = new List<Transform>(_objectsProvider.AliveEnemies);
            sortedEnemiesRico.Remove(_ricoshetFrom); // remove self from list
            sortedEnemiesRico.Sort(CompareDistancesRicoshet);

            foreach (var enemy in sortedEnemiesRico)
            {
                if (IsVisible(_ricoshetFrom, enemy))
                {
                    ricoshetTo = enemy;
                    _enemyAndItsClosest.Add(_ricoshetFrom, enemy);
                    
                    return ricoshetTo;
                }
            }

            return ricoshetTo;
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
                if (IsVisible(_objectsProvider.Character.transform,enemy))
                {
                    _currentClosestAndVisible = enemy;
                    break;
                }
            }
            
            return _currentClosestAndVisible;
        }

        private bool IsVisible(Transform start,Transform target)
        {
            Transform character = start;

            if (Physics.Linecast(character.position, target.position,out RaycastHit _,_layerToCheckFor,QueryTriggerInteraction.UseGlobal))
            {
                Debug.DrawLine(character.position,target.position,Color.red);
                return false;
            }
            else
            {
                Debug.DrawLine(character.position,target.position,Color.green);
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
                return -1;
        
            if (nextDistance < prevDistance)
                return 1;
        
            return 0;
        }
        
        private int CompareDistancesRicoshet(Transform prevEnemy, Transform nextEnemy)
        {
            Transform character = _objectsProvider.Character.transform;
            var ricochetFrom = _ricoshetFrom.position;
            var prevDistance = (ricochetFrom - prevEnemy.position).sqrMagnitude;
            var nextDistance = (ricochetFrom - nextEnemy.position).sqrMagnitude;

            if (nextDistance > prevDistance)
                return -1;
        
            if (nextDistance < prevDistance)
                return 1;
        
            return 0;
        }
    }
}
