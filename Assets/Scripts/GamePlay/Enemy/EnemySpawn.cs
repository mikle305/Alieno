using GamePlay.Other.Ids;
using UnityEngine;

namespace GamePlay.Enemy
{
    public class EnemySpawn : MonoBehaviour
    {
        [field: SerializeField] public EnemyId Id { get; private set; }
        [field: SerializeField, Min(0)] public float HealthMultiplier { get; private set; } = 1;
    }
}