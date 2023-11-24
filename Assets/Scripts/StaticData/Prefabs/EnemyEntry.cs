using System;
using GamePlay.Enemy;
using UnityEngine;

namespace StaticData
{
    [Serializable]
    public class EnemyEntry
    {
        [field: SerializeField] public EnemyId Id { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
    }
}