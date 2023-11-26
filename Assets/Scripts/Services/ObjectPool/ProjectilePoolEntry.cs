﻿using System;
using UnityEngine;

namespace Services.ObjectPool
{
    [Serializable]
    public class ProjectilePoolEntry
    {
        [field: SerializeField] public ProjectileId Id { get; private set; }
        [field: SerializeField] public int StartCount { get; private set; }
    }
}