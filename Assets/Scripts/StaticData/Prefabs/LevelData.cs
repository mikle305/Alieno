using System;
using GamePlay.Other;
using UnityEngine;

namespace StaticData
{
    [Serializable]
    public class LevelData
    {
        [field: SerializeField] public RoomsMap Map { get; private set; }
        [field: SerializeField] public Room[] Rooms { get; private set; }
    }
}