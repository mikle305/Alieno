using System;
using UnityEngine;

namespace GamePlay.Abilities
{
    [Serializable]
    public class ShotLevelData : AbilityLevelData
    {
        [field: SerializeField] public Vector3[] ShotsDirections { get; private set; } = { };
    }
}