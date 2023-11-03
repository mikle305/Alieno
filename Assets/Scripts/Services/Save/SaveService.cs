using System;
using Additional.Game;
using SaveData;

namespace Services.Save
{
    public class SaveService : MonoSingleton<SaveService>
    {
        private const string _progressKey = "Progress";
        private ISaveStorage<PlayerProgress> _storage;
        
        public PlayerProgress Progress { get; private set; }
        public event Action ProgressLoaded;


        private void Start()
        {
            _storage = new PlayerPrefsStorage<PlayerProgress>(_progressKey);
        }
        
        public void Save()
        {
            _storage.Save(Progress);
        }

        public void Load()
        {
            Progress = _storage.Load() ?? new PlayerProgress();
            ProgressLoaded?.Invoke();
        }
    }
}