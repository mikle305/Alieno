using Additional.Constants;
using Additional.Game;
using StaticData;
using StaticData.Music;
using UnityEngine;

namespace Services
{
    public class StaticDataService : MonoSingleton<StaticDataService>
    {
        private MusicConfig _musicConfig;
        private AppConfig _appConfig;


        public MusicConfig GetMusicConfig()
            => _musicConfig ??= LoadData<MusicConfig>(StaticDataPaths.MusicConfig);

        public AppConfig GetAppConfig()
            => _appConfig ??= LoadData<AppConfig>(StaticDataPaths.AppConfig);

        private static T LoadData<T>(string path) 
            where T : Object
            => Resources.Load<T>(path);
    }
}