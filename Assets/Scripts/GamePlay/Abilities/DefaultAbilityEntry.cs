using System;
using UnityEngine;

namespace GamePlay.Abilities
{
    [Serializable]
    public class DefaultAbilityEntry
    {
        [field: SerializeField] public AbilityId Id { get; private set; }
        [field: SerializeField, Min(1)] public int Level { get; private set; } = 1;
    }
}