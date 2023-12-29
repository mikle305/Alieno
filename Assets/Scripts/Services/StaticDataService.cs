using Additional.Constants;
using StaticData.Abilities;
using StaticData.Enemies;
using StaticData.GameConfig;
using StaticData.Music;
using StaticData.Pools;
using StaticData.Prefabs;
using StaticData.UI;
using UnityEngine;

namespace Services
{
    public class StaticDataService
    {
        private MusicConfig _musicConfig;
        private PrefabsConfig _prefabsConfig;
        private UiConfig _uiConfig;
        private AbilitiesConfig _abilitiesConfig;
        private PoolsConfig _poolsConfig;
        private GamePlayConfig _gamePlayConfig;
        private EnemiesConfig _enemiesConfig;


        public MusicConfig GetMusicConfig()
            => _musicConfig ??= LoadData<MusicConfig>(StaticDataPaths.MusicConfig);

        public PrefabsConfig GetPrefabsConfig()
            => _prefabsConfig ??= LoadData<PrefabsConfig>(StaticDataPaths.PrefabsConfig);

        public UiConfig GetUiConfig()
            => _uiConfig ??= LoadData<UiConfig>(StaticDataPaths.UiConfig);
        
        public AbilitiesConfig GetAbilitiesConfig()
            => _abilitiesConfig ??= LoadData<AbilitiesConfig>(StaticDataPaths.AbilitiesConfig);

        public PoolsConfig GetPoolsConfig()
            => _poolsConfig ??= LoadData<PoolsConfig>(StaticDataPaths.PoolsConfig);

        public EnemiesConfig GetEnemiesConfig()
            => _enemiesConfig ??= LoadData<EnemiesConfig>(StaticDataPaths.EnemiesConfig);

        public GamePlayConfig GetGamePlayConfig()
            => _gamePlayConfig ??= LoadData<GamePlayConfig>(StaticDataPaths.GamePlayConfig);


        private static T LoadData<T>(string path)
            where T : Object
            => Resources.Load<T>(path);

        private static T[] LoadAllData<T>(string folder)
            where T : Object
            => Resources.LoadAll<T>(folder);
    }
}