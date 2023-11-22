using System;
using GamePlay.Enemy;
using Services;
using UnityEngine;

namespace StaticData
{
    [Serializable]
    public class EnemyMapEntry
    {
        [field: SerializeField] public EnemyId EnemyId { get; private set; }
        [field: SerializeField] public PoolId PoolId { get; private set; }
    }
}