using System;
using Additional.Constants;
using Additional.Game;
using SaveData;

namespace Services
{
    public class MenuService : MonoSingleton<MenuService>
    {
        public event Action PlayInvoked;
        public event Action<PlayerData> ProgressReceived;
        

        private void Update()
        {
            if (SimpleInput.GetButtonDown(InputConstants.Play))
                PlayInvoked?.Invoke();
        }

        public void DisplayProgress(PlayerData playerProgress)
            => ProgressReceived?.Invoke(playerProgress);
    }
}