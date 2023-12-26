using UnityEngine;

namespace StaticData.GameConfig
{
    [CreateAssetMenu(menuName = "StaticData/GamePlay Config", fileName = "GamePlayConfig")]
    public class GamePlayConfig : ScriptableObject
    {
        [field: SerializeField] public LayerMask ObstacleLayer { get; private set; }
        [field: SerializeField] public LevelMapData LevelMapData { get; private set; }
        [field: SerializeField] public TransparentObstaclesData TransparentObstaclesData { get; private set; }
    }
}