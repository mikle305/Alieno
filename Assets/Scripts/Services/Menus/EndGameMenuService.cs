using System;
using Additional.Constants;
using VContainer.Unity;

namespace Services
{
    public class EndGameMenuService : ITickable
    {
        public event Action ToMainMenuInvoked;
        public event Action DefeatReceived;


        public void Tick()
        {
            if (!SimpleInput.GetButtonDown(InputConstants.MainMenu))
                return;
                
            ToMainMenuInvoked?.Invoke();
        }

        public void InvokeDefeat()
            => DefeatReceived?.Invoke();
    }
}