using UI.GamePlay;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(menuName = "StaticData/Prefabs Config", fileName = "PrefabsConfig")]
    public class PrefabsConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject Character { get; private set; }
        [field: SerializeField] public Hud Hud { get; private set; }
        [field: SerializeField] public GameObject Marker { get; private set; }
        [field: SerializeField] public LevelEntry[] Levels { get; private set; }
        [field: SerializeField] public EnemyEntry[] Enemies { get; private set; }
        [field: SerializeField] public ProjectileEntry[] Projectiles { get; private set; }
    }
}