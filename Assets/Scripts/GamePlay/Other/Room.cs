using Additional.Extensions;
using GamePlay.Enemy;
using UnityEngine;

namespace GamePlay.Other
{
    public class Room : MonoBehaviour
    {
        [SerializeField] private Transform _enemySpawnsParent;
        [field: SerializeField] public Transform EntryPoint { get; private set; }
        [field: SerializeField] public GameObject ExitPoint { get; private set; }

        public EnemySpawn[] EnemySpawns => _enemySpawnsParent.GetChildren<EnemySpawn>();
    }
}
