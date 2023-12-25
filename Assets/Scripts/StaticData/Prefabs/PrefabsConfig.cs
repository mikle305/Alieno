using System.Collections.Generic;
using System.Linq;
using GamePlay.Other.Ids;
using UI.GamePlay;
using UnityEngine;

namespace StaticData.Prefabs
{
    [CreateAssetMenu(menuName = "StaticData/Prefabs Config", fileName = "PrefabsConfig")]
    public class PrefabsConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject Character { get; private set; }
        [field: SerializeField] public Hud Hud { get; private set; }
        [field: SerializeField] public AbilityHelpElement AbilityHelpElement { get; private set; }
        [field: SerializeField] public LevelEntry[] Levels { get; private set; }
        
        [SerializeField] private PrefabEntry<EnemyId>[] _enemies;
        [SerializeField] private PrefabEntry<ProjectileId>[] _projectiles;
        [SerializeField] private PrefabEntry<UiElementId>[] _uiElements;
        
        
        private Dictionary<ProjectileId, GameObject> _projectilesMap;
        private Dictionary<EnemyId, GameObject> _enemiesMap;
        private Dictionary<UiElementId, GameObject> _uiElementsMap;
        
        
        public GameObject GetProjectile(ProjectileId id)
            => (_projectilesMap ??= CreateProjectilesMap())[id];

        public GameObject GetEnemy(EnemyId id)
            => (_enemiesMap ??= CreateEnemiesMap())[id];

        public GameObject GetUiElement(UiElementId id)
            => (_uiElementsMap ??= CreateUiElementsMap())[id];


        private Dictionary<ProjectileId, GameObject> CreateProjectilesMap() 
            => _projectiles.ToDictionary(x => x.Id, x => x.Prefab);

        private Dictionary<EnemyId, GameObject> CreateEnemiesMap() 
            => _enemies.ToDictionary(x => x.Id, x => x.Prefab);

        private Dictionary<UiElementId, GameObject> CreateUiElementsMap()
            => _uiElements.ToDictionary(x => x.Id, x => x.Prefab);
    }
}