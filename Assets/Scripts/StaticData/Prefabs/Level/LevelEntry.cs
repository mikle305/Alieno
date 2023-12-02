using System;
using UnityEngine;

namespace StaticData.Prefabs
{
    [Serializable]
    public class LevelEntry
    {
        [field: SerializeField] public RoomsMap Map { get; private set; }
        [field: SerializeField] public Room[] Rooms { get; private set; }
    }
}