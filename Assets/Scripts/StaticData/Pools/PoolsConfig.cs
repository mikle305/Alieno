using GamePlay.Other.Ids;
using UnityEngine;

namespace StaticData.Pools
{
    [CreateAssetMenu(menuName = "StaticData/Pools Config", fileName = "PoolsConfig")]
    public class PoolsConfig : ScriptableObject
    {
        [field: SerializeField] public PoolEntry<ProjectileId>[] ProjectilePools { get; private set; } = { };
        [field: SerializeField] public PoolEntry<UiElementId>[] UiElementPools { get; private set; } = { };
    }
}