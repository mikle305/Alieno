using System;
using Additional.Constants;
using Additional.Game;
using GamePlay.Abilities;
using SaveData;

namespace Services.Save
{
    public class SaveService : MonoSingleton<SaveService>
    {
        private const string _progressKey = "Progress";
        private ISaveStorage<Progress> _storage;
        
        public Progress Progress { get; private set; }
        public event Action ProgressLoaded;
        public event Action ProgressSaved;


        private void Start() 
            => _storage = new PlayerPrefsStorage<Progress>(_progressKey);

        public void Save()
        {
            _storage.Save(Progress);
            ProgressSaved?.Invoke();
        }

        public void Load()
        {
            Progress = _storage.Load() ?? new Progress();
            ProgressLoaded?.Invoke();
        }

        public void ResetRoomsProgress()
        {
            PlayerData playerProgress = Progress.PlayerData;
            playerProgress.AbilitySelected = true;
            playerProgress.GeneratedAbilities = Array.Empty<AbilityId>();
            playerProgress.CurrentAbilities = DefaultPlayerProgress.GetAbilities();
            playerProgress.CurrentHealth = DefaultPlayerProgress.Health;
            playerProgress.Room = DefaultPlayerProgress.Room;
        }

        public void ResetLevelProgress()
        {
            PlayerData playerProgress = Progress.PlayerData;
            playerProgress.Level = DefaultPlayerProgress.Level;
        }
    }
}