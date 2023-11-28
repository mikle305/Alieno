using Additional.Constants;
using Cysharp.Threading.Tasks;
using GameFlow.Context;
using GamePlay.Other;
using Services;
using Services.Factories;
using Services.Save;
using UnityEngine;

namespace GameFlow.States
{
    public class SceneLoadingState : State
    {
        private readonly GameStateMachine _context;
        private readonly SceneLoader _sceneLoader;
        private readonly ObjectsProvider _objectsProvider;
        private readonly SaveService _saveService;
        
        private GameFactory _gameFactory;


        public SceneLoadingState(GameStateMachine context)
        {
            _context = context;
            _sceneLoader = SceneLoader.Instance;
            _saveService = SaveService.Instance;
            _objectsProvider = ObjectsProvider.Instance;
        }

        public override void Enter() 
            => _sceneLoader.Load(SceneNames.Level, OnLevelLoaded);

        private void OnLevelLoaded()
            => OnLevelLoadedAsync().Forget();

        private async UniTask OnLevelLoadedAsync()
        {
            await Construct();
            
            InitCharacter();
            InitMarker();
            InitRoomsMap();
            InitRooms();
            
            EnterProgressRestore();
        }

        private async UniTask Construct()
        {
            await UniTask.Yield();
            _gameFactory = GameFactory.Instance;
        }

        private void InitCharacter()
        {
            GameObject character = _gameFactory.CreateCharacter();
            character.SetActive(false);
            _objectsProvider.Character = character;
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

        private void InitMarker()
        {
            GameObject marker = _gameFactory.CreateMarker();
            marker.SetActive(false);
            _objectsProvider.Marker = marker;
        }

        private void EnterProgressRestore() 
            => _context.Enter<ProgressRestoreState>();

        private int GetCurrentLevel() 
            => _saveService.Progress.PlayerData.Level;

        private int GetCurrentRoom() 
            => _saveService.Progress.PlayerData.Room;
    }
}