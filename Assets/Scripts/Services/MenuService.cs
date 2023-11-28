using System;
using Additional.Constants;
using Additional.Game;

namespace Services
{
    public class MenuService : MonoSingleton<MenuService>
    {
        public event Action PlayInvoked;


        private void Update()
        {
            if (SimpleInput.GetButtonDown(InputConstants.Play))
                PlayInvoked?.Invoke();
        }
    }
}