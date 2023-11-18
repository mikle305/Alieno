using System;
using UnityEngine;

namespace GamePlay.Abilities
{
    [Serializable]
    public class DefaultAbilityEntry
    {
        [field: SerializeField] public AbilityId Id { get; private set; }
        [field: SerializeField] public int UpLevelTimes { get; private set; }
    }
}