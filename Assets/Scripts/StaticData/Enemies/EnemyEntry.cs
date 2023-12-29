using System;
using GamePlay.Other.Ids;
using UnityEngine;

namespace StaticData.Enemies
{
    [Serializable]
    public class EnemyEntry
    {
        [field: SerializeField] public EnemyId Id { get; private set; }
        [field: SerializeField, Min(0)] public float Health { get; private set; }
    }
}