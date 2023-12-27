using UnityEngine;

namespace Additional.Constants
{
    public static class GameConstants
    {
        public const float MusicTransitionDuration = 1.5f;
        public const string VolumeMixerParam = "Volume";
        public const string SoundMixerParam = "SFX";
        public const string MusicMixerParam = "Music";

        public const string PrefsProgressKey = "_Progress";
        
        public const float EntitiesPivotOffset = 1.25f;
        public static readonly int EnemyLayer = 1 << LayerMask.NameToLayer("Enemy");
        public static readonly int PlayerLayer= 1 << LayerMask.NameToLayer("Player");
        public static readonly int ObstacleLayer = 1 << LayerMask.NameToLayer("Obstacle");
    }
}