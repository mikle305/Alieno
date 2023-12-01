using GamePlay.Other.Ids;
using UnityEngine;

namespace GamePlay.Enemy
{
    public class EnemySpawn : MonoBehaviour
    {
        [field: SerializeField] public EnemyId Id { get; private set; }
    }
}