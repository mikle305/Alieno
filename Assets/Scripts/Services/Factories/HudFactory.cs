using Additional.Game;
using GamePlay.Characteristics;
using UI.GamePlay;
using UnityEngine;

namespace Services
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
            CharacteristicHudView view = hud.HealthView;
            var presenter = new CharacteristicPresenter(health, view);
            view.Init(presenter);
        }
    }
}