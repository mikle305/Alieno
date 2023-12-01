using System;
using GamePlay.Other;
using GamePlay.Other.Ids;
using Services;
using Services.ObjectPool;
using UnityEngine;

namespace StaticData
{
    [Serializable]
    public class ProjectileEntry
    {
        [field: SerializeField] public ProjectileId Id { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
    }
}