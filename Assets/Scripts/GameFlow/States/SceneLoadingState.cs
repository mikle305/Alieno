using Additional.Constants;
using GameFlow.Context;
using GamePlay.Other.Navigators;
using GamePlay.UnitsComponents;
using Services;
using Services.Factories;
using Services.ObjectPool;
using Services.Save;
using StaticData.Prefabs;
using UnityEngine;

namespace GameFlow.States
{
    public class SceneLoadingState : State
    {
        private readonly GameStateMachine _context;
        private readonly SceneLoader _sceneLoader;
        private readonly ObjectsProvider _objectsProvider;
        private readonly SaveService _saveService;
        private readonly ObjectPoolsProvider _poolsProvider;
        private readonly GameFactory _gameFactory;


        public SceneLoadingState(GameStateMachine context, SceneLoader sceneLoader, ObjectPoolsProvider poolsProvider,
            GameFactory gameFactory)
        {
            _context = context;
            _sceneLoader = sceneLoader;
            _poolsProvider = poolsProvider;
            _gameFactory = gameFactory;
            _saveService = SaveService.Instance;
            _objectsProvider = ObjectsProvider.Instance;
        }

        public override void Enter()
            => _sceneLoader.Load(SceneNames.Level, OnLevelLoaded);

        private void OnLevelLoaded()
        {
            _poolsProvider.InitPools();

            InitCharacter();
            InitDirectionArrow();
            InitRoomsMap();
            InitRooms();

            EnterProgressRestore();
        }

        private void InitCharacter()
        {
            GameObject character = _gameFactory.CreateCharacter();
            SubscribeCharacterDeath(character);
            character.SetActive(false);
            _objectsProvider.Character = character;
            _objectsProvider.CharacterRigidbody = character.GetComponent<Rigidbody>();
        }

        private void InitDirectionArrow()
        {
            var directionArrow = _objectsProvider.Character.GetComponentInChildren<DirectionArrow>();
            directionArrow.gameObject.SetActive(false);
            _objectsProvider.DirectionArrow = directionArrow;
        }

        private void InitRoomsMap()
        {
            int currentLevel = GetCurrentLevel();
            RoomsMap roomsMap = _gameFactory.CreateRoomsMap(currentLevel);
            roomsMap.gameObject.SetActive(false);
            _objectsProvider.RoomsMap = roomsMap;
        }

        private void InitRooms()
        {
            int currentLevel = GetCurrentLevel();
            int currentRoom = GetCurrentRoom();
            Room[] rooms = _gameFactory.CreateRooms(currentLevel, currentRoom);

            for (int i = currentRoom - 1; i < rooms.Length; i++)
                rooms[i].gameObject.SetActive(false);

            _objectsProvider.Rooms = rooms;
        }

        private void SubscribeCharacterDeath(GameObject character)
        {
            var characterDeath = character.GetComponent<Death>();
            characterDeath.Happened += EnterDefeat;
        }

        private void EnterDefeat()
            => _context.Enter<DefeatState>();

        private void EnterProgressRestore()
            => _context.Enter<ProgressRestoreState>();

        private int GetCurrentLevel()
            => _saveService.Progress.PlayerData.Level;

        private int GetCurrentRoom()
            => _saveService.Progress.PlayerData.Room;
    }
}