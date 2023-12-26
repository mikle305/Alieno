using System;
using UnityEngine;

namespace StaticData.Pools
{
    [Serializable]
    public class PoolEntry<TId>
    {
        [field: SerializeField] public TId Id { get; private set; }
        [field: SerializeField] public int StartCount { get; private set; }
    }
}