namespace Services
{
    public class MobileInputService : InputServiceBase
    {
        private readonly ObjectsProvider _objectsProvider;


        public MobileInputService(ObjectsProvider objectsProvider)
        {
            _objectsProvider = objectsProvider;
            _objectsProvider.HudLoaded += EnableMobileInputWindow;
        }

        private void EnableMobileInputWindow()
        {
            if (_objectsProvider.Hud != null)
                _objectsProvider.Hud.MobileInputWindow.SetActive(true);
        }
    }
}