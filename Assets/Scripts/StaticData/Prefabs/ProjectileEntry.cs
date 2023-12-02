using System;
using GamePlay.Other.Ids;
using UnityEngine;

namespace StaticData.Prefabs
{
    [Serializable]
    public class ProjectileEntry
    {
        [field: SerializeField] public ProjectileId Id { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
    }
}