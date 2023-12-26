using System;
using UnityEngine;

namespace StaticData.GameConfig
{
    [Serializable]
    public class LevelMapData
    {
        [field: SerializeField] public Vector3 Offset { get; private set; } = new(0, 2, 0);
        [field: SerializeField] public float Speed { get; private set; } = 10;
    }
}