﻿using StaticData.Prefabs;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Services.Factories
{
    public class GameFactory
    {
        private readonly StaticDataService _staticDataService;
        private readonly IObjectResolver _monoResolver;


        public GameFactory(StaticDataService staticDataService, IObjectResolver monoResolver)
        {
            _monoResolver = monoResolver;
            _staticDataService = staticDataService;
        }

        public GameObject CreateCharacter()
        {
            GameObject characterPrefab = _staticDataService.GetPrefabsConfig().Character;
            return _monoResolver.Instantiate(characterPrefab);
        }

        public RoomsMap CreateRoomsMap(int level)
        {
            RoomsMap roomsMapPrefab = _staticDataService.GetPrefabsConfig().Levels[level - 1].Map;
            return _monoResolver.Instantiate(roomsMapPrefab);
        }

        public Room[] CreateRooms(int level, int room)
        {
            Room[] roomsPrefabs = _staticDataService.GetPrefabsConfig().Levels[level - 1].Rooms;
            var rooms = new Room[roomsPrefabs.Length];
            for (int i = room - 1; i < roomsPrefabs.Length; i++)
                rooms[i] = _monoResolver.Instantiate(roomsPrefabs[i], Vector3.zero, Quaternion.Euler(0, 180, 0));

            return rooms;
        }
    }
}