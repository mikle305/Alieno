using Additional.Game;
using UnityEngine;

namespace Services
{
    public class GameFactory : MonoSingleton<GameFactory>
    {
        private StaticDataService _staticDataService;

        
        private void Start()
        {
            _staticDataService = StaticDataService.Instance;
        }

        public GameObject CreateCharacter()
        {
            GameObject characterPrefab = _staticDataService.GetPrefabsConfig().Character;
            return Instantiate(characterPrefab);
        }

        public GameObject CreateMarker()
        {
            GameObject markerPrefab = _staticDataService.GetPrefabsConfig().Marker;
            return Instantiate(markerPrefab);
        }
    }
}