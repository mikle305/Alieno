using System.Collections.Generic;
using System.Linq;
using Additional.Constants;
using Additional.Game;
using GamePlay.Abilities;
using StaticData;
using StaticData.Abilities;
using StaticData.Music;
using UnityEngine;

namespace Services
{
    public class StaticDataService : MonoSingleton<StaticDataService>
    {
        private MusicConfig _musicConfig;
        private PrefabsConfig _prefabsConfig;
        private PoolIdsConfig _poolIdsConfig;
        private Dictionary<AbilityId, AbilityData> _abilitiesMap;


        public MusicConfig GetMusicConfig()
            => _musicConfig ??= LoadData<MusicConfig>(StaticDataPaths.MusicConfig);

        public PrefabsConfig GetPrefabsConfig()
            => _prefabsConfig ??= LoadData<PrefabsConfig>(StaticDataPaths.AppConfig);

        public PoolIdsConfig GetPoolIdsConfig()
            => _poolIdsConfig ??= LoadData<PoolIdsConfig>(StaticDataPaths.PoolIdsConfig);

        public AbilityData GetAbility(AbilityId id)
            => (_abilitiesMap ??= LoadAbilities()).GetValueOrDefault(id);

        
        private static Dictionary<AbilityId, AbilityData> LoadAbilities()
            => LoadData<AbilitiesConfig>(StaticDataPaths.AbilitiesConfig)
                .AbilitiesData
                .ToDictionary(a => a.Id, a => a);

        private static T LoadData<T>(string path)
            where T : Object
            => Resources.Load<T>(path);

        private static T[] LoadAllData<T>(string folder)
            where T : Object
            => Resources.LoadAll<T>(folder);
    }
}