using System;
using Additional.Constants;
using Additional.Game;

namespace Services
{
    public class EndGameMenuService : MonoSingleton<EndGameMenuService>
    {
        public event Action ToMainMenuInvoked;
        public event Action DefeatReceived;
        
        
        private void Update()
        {
            if (!SimpleInput.GetButtonDown(InputConstants.MainMenu))
                return;
                
            ToMainMenuInvoked?.Invoke();
        }

        public void InvokeDefeat()
            => DefeatReceived?.Invoke();
    }
}