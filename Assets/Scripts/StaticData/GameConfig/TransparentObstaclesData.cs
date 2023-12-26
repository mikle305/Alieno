using System;
using UnityEngine;

namespace StaticData.GameConfig
{
    [Serializable]
    public class TransparentObstaclesData
    {
        [field: SerializeField] public Vector3 BoxHalfSize { get; private set; } = new(4, 0.5f, 4);
        [field: SerializeField] public Vector3 Offset { get; private set; } = new(0, -1, 0);
    }
}