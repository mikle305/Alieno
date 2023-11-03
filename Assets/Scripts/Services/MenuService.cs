using Additional.Constants;
using Additional.Game;

namespace Services
{
    public class MenuService : MonoSingleton<MenuService>
    {
        public bool IsPlayInvoked()
            => SimpleInput.GetButtonDown(InputConstants.Play);
    }
}