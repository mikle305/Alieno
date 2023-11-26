using Additional.Constants;
using Cysharp.Threading.Tasks;
using GameFlow.Context;
using GamePlay.Other;
using Services;
using Services.Factories;
using Services.Save;
using UI.GamePlay;
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
        private HudFactory _hudFactory;


        public SceneLoadingState(GameStateMachine context)
        {
            _context = context;
            _sceneLoader = SceneLoader.Instance;
            _saveService = SaveService.Instance;
            _objectsProvider = ObjectsProvider.Instance;
        }

        public override void Enter() 
            => _sceneLoader.Load(SceneNames.Level, OnLevelLoaded);

        public override void Exit()
        {
        }

        private void OnLevelLoaded()
            => OnLevelLoadedAsync().Forget();

        private async UniTask OnLevelLoadedAsync()
        {
            await UniTask.DelayFrame(2);
            Construct();
            
            InitCharacter();
            InitMarker();
            InitRoomsMap();
            InitRooms();
            InitHud();
            
            EnterRoomSelectionState();
        }

        private void Construct()
        {
            _gameFactory = GameFactory.Instance;
            _hudFactory = HudFactory.Instance;
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
            Room[] rooms = _gameFactory.CreateRooms(currentLevel);
            foreach (Room room in rooms) 
                room.gameObject.SetActive(false);

            _objectsProvider.Rooms = rooms;
        }

        private void InitMarker()
        {
            GameObject marker = _gameFactory.CreateMarker();
            marker.SetActive(false);
            _objectsProvider.Marker = marker;
        }

        private void InitHud()
        {
            GameObject character = _objectsProvider.Character;
            Hud hud = _hudFactory.Create(character);
            hud.gameObject.SetActive(false);
            _objectsProvider.Hud = hud;
        }

        private void EnterRoomSelectionState() 
            => _context.Enter<RoomSelectionState>();

        private int GetCurrentLevel() 
            => _saveService.Progress.PlayerData.Level;
    }
}