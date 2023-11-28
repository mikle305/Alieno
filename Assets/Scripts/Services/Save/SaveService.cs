using System;
using Additional.Game;
using SaveData;

namespace Services.Save
{
    public class SaveService : MonoSingleton<SaveService>
    {
        private const string _progressKey = "Progress";
        private ISaveStorage<Progress> _storage;
        
        public Progress Progress { get; private set; }
        public event Action ProgressLoaded;


        private void Start()
        {
            _storage = new PlayerPrefsStorage<Progress>(_progressKey);
        }
        
        public void Save()
        {
            _storage.Save(Progress);
            print(Progress.PlayerData.Room);
        }

        public void Load()
        {
            Progress = _storage.Load() ?? new Progress();
            ProgressLoaded?.Invoke();
        }
    }
}