using Additional.Game;
using GamePlay.Characteristics;
using UI.GamePlay;
using UnityEngine;

namespace Services
{
    public class HudFactory : MonoSingleton<HudFactory>
    {
        private ObjectsProvider _objectsProvider;
        

        private void Start()
        {
            _objectsProvider = ObjectsProvider.Instance;
        }


        public void CreateHud(GameObject character)
        {
            InitHealth(character);
        }

        private void InitHealth(GameObject character)
        {
            var health = character.GetComponent<HealthData>();
            ICharacteristicView view = _objectsProvider.Hud.HealthView;
            var presenter = new CharacteristicPresenter(health, view);
            view.Init(presenter);
        }
    }
}