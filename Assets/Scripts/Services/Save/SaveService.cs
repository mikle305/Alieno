using System;
using Additional.Constants;
using Additional.Game;
using GamePlay.Abilities;
using SaveData;

namespace Services.Save
{
    public class SaveService
    {
        private readonly ISaveStorage<Progress> _saveStorage;

        public Progress Progress { get; private set; }
        public event Action ProgressLoaded;
        public event Action ProgressSaved;


        public SaveService(ISaveStorage<Progress> saveStorage)
        {
            _saveStorage = saveStorage;
        }

        public void Save()
        {
            _saveStorage.Save(Progress);
            ProgressSaved?.Invoke();
        }

        public void Load()
        {
            Progress = _saveStorage.Load() ?? new Progress();
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