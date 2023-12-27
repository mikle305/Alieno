using GamePlay.Characteristics;
using GamePlay.Player;
using UI.GamePlay;
using UnityEngine;

namespace Services.Factories
{
    public class HudFactory
    {
        private readonly StaticDataService _staticDataService;
        private readonly ObjectActivator _objectActivator;


        public HudFactory(StaticDataService staticDataService, ObjectActivator objectActivator)
        {
            _objectActivator = objectActivator;
            _staticDataService = staticDataService;
        }
        
        public Hud Create(GameObject character)
        {
            Hud hud = CreateHud();
            
            InitHealth(hud, character);
            InitDash(hud, character);

            return hud;
        }

        private Hud CreateHud()
        {
            Hud hudPrefab = _staticDataService.GetPrefabsConfig().Hud;
            return _objectActivator.Instantiate(hudPrefab);
        }

        private void InitHealth(Hud hud, GameObject character)
        {
            var health = character.GetComponent<HealthData>();
            CharacteristicView view = hud.HealthView;
            var presenter = new CharacteristicPresenter(health, view);
            presenter.Bind(forceUpdate: true);
            view.Init(presenter);
        }

        private void InitDash(Hud hud, GameObject character)
        {
            var dash = character.GetComponent<PlayerDash>();
            CharacteristicView view = hud.DashView;
            var presenter = new DashPresenter(dash, view);
            view.Init(presenter);
        }
    }
}