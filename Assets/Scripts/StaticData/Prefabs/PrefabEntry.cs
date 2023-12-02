using System;
using UnityEngine;

namespace StaticData.Prefabs
{
    [Serializable]
    public class PrefabEntry<TId>
    {
        [field: SerializeField] public TId Id { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
    }
}