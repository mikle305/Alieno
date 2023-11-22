using UnityEngine;

namespace StaticData
{
    
    [CreateAssetMenu(menuName = "StaticData/Pool Ids Config", fileName = "PoolIdsConfig")]
    public class PoolIdsConfig : ScriptableObject
    {
        [field: SerializeField] public EnemyMapEntry[] EnemyEntries { get; private set; }
    }
}