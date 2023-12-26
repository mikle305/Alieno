using System;
using GamePlay.Abilities;
using UnityEngine;

namespace StaticData.Abilities
{
    [Serializable]
    public class PlayerAbilityData
    {
        [field: SerializeField] public AbilityId AbilityId { get; private set; }
        [field: SerializeField] public int[] Levels { get; private set; } = new[] { 1 };
    }
}