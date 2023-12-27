using StaticData.Prefabs;
using UnityEngine;

namespace Services.Factories
{
    public class GameFactory
    {
        private readonly StaticDataService _staticDataService;
        private readonly ObjectActivator _objectActivator;


        public GameFactory(StaticDataService staticDataService, ObjectActivator objectActivator)
        {
            _objectActivator = objectActivator;
            _staticDataService = staticDataService;
        }

        public GameObject CreateCharacter()
        {
            GameObject characterPrefab = _staticDataService.GetPrefabsConfig().Character;
            return _objectActivator.Instantiate(characterPrefab);
        }

        public RoomsMap CreateRoomsMap(int level)
        {
            RoomsMap roomsMapPrefab = _staticDataService.GetPrefabsConfig().Levels[level - 1].Map;
            return _objectActivator.Instantiate(roomsMapPrefab);
        }

        public Room[] CreateRooms(int level, int room)
        {
            Room[] roomsPrefabs = _staticDataService.GetPrefabsConfig().Levels[level - 1].Rooms;
            var rooms = new Room[roomsPrefabs.Length];
            for (int i = room - 1; i < roomsPrefabs.Length; i++)
                rooms[i] = _objectActivator.Instantiate(roomsPrefabs[i], Vector3.zero, Quaternion.Euler(0, 180, 0));

            return rooms;
        }
    }
}