using Additional.Game;
using GamePlay.Other;
using UnityEngine;

namespace Services.Factories
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

        public RoomsMap CreateRoomsMap(int level)
        {
            RoomsMap roomsMapPrefab = _staticDataService.GetPrefabsConfig().Levels[level - 1].Map;
            return Instantiate(roomsMapPrefab);
        }

        public Room[] CreateRooms(int level, int room)
        {
            Room[] roomsPrefabs = _staticDataService.GetPrefabsConfig().Levels[level - 1].Rooms;
            var rooms = new Room[roomsPrefabs.Length];
            for (int i = room - 1; i < roomsPrefabs.Length; i++)
                rooms[i] = Instantiate(roomsPrefabs[i], Vector3.zero, Quaternion.Euler(0, 180, 0));

            return rooms;
        }
    }
}