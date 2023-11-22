using System;
using UnityEngine;

namespace Services
{
    [Serializable]
    public class PoolEntry
    {
        [field: SerializeField] public ObjectId Id { get; private set; }
        [field: SerializeField] public Poolable Prefab { get; private set; }
        [field: SerializeField] public int StartCount { get; private set; }
    }
}