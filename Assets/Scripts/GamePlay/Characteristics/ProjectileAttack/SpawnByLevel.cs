using System;
using System.Linq;
using UnityEngine;

namespace GamePlay.Characteristics
{
    [Serializable]
    public class SpawnByLevel
    {
        [SerializeField] private Transform[] _spawns = { };

        public Vector3[] Spawns => _spawns.Select(s => s.position).ToArray();
    }
}