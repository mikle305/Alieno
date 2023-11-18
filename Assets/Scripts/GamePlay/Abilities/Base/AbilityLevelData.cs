using System;
using TriInspector;
using UnityEngine;

namespace GamePlay.Abilities
{
    [Serializable]
    public class AbilityLevelData
    {
        [field: SerializeField, ReadOnly] public int Id { get; private set; }
    }
}