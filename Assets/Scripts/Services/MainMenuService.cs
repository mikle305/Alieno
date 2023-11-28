using System;
using Additional.Constants;
using Additional.Game;
using SaveData;

namespace Services
{
    public class MainMenuService : MonoSingleton<MainMenuService>
    {
        public event Action StartGameInvoked;
        public event Action<PlayerData> ProgressReceived;
        public event Action PlayClicked;
        

        private void Update()
        {
            if (SimpleInput.GetButtonDown(InputConstants.Play)) 
                PlayClicked?.Invoke();
        }
        
        public void StartGame()
            => StartGameInvoked?.Invoke();

        public void DisplayProgress(PlayerData playerProgress)
            => ProgressReceived?.Invoke(playerProgress);
    }
}