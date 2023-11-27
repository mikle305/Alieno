using Additional.Game;
using GamePlay.Characteristics;
using GamePlay.Player;
using UI.GamePlay;
using UnityEngine;

namespace Services.Factories
{
    public class HudFactory : MonoSingleton<HudFactory>
    {
        private StaticDataService _staticDataService;


        private void Start()
        {
            _staticDataService = StaticDataService.Instance;
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
            return Instantiate(hudPrefab);
        }

        private void InitHealth(Hud hud, GameObject character)
        {
            var health = character.GetComponent<HealthData>();
            CharacteristicView view = hud.HealthView;
            var presenter = new CharacteristicPresenter(health, view);
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