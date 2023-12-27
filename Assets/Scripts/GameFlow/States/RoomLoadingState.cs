using System.Collections.Generic;
using Cinemachine;
using GamePlay.Enemy;
using Services;
using Services.Factories;
using Services.Save;
using StaticData.Prefabs;
using UnityEngine;

namespace GameFlow.States
{
    public class RoomLoadingState : State
    {
        private readonly GameStateMachine _context;
        private readonly SaveService _saveService;
        private readonly ObjectsProvider _objectsProvider;
        private readonly EnemiesDeathObserver _enemiesDeathObserver;
        private readonly MusicService _musicService;
        private readonly EnemyFactory _enemyFactory;
        
        private Room _currentRoom;


        public RoomLoadingState(
            GameStateMachine context, 
            MusicService musicService, 
            EnemyFactory enemyFactory)
        {
            _context = context;
            _enemyFactory = enemyFactory;
            _saveService = SaveService.Instance;
            _objectsProvider = ObjectsProvider.Instance;
            _enemiesDeathObserver = EnemiesDeathObserver.Instance;
            _musicService = musicService;
        }

        public override void Enter()
        {
            InitCurrentRoom();
            SwitchToRoomMusic();
            SpawnPlayer();
            MoveCameraToPlayer();
            SpawnEnemies();
            ShowDependentObjects();
            InitDependentServices();
        }

        public override void Exit() 
            => _enemiesDeathObserver.AllCleared -= EnterRoomCleared;

        private void EnterRoomCleared() 
            => _context.Enter<RoomClearedState>();

        private void InitCurrentRoom()
        {
            int room = _saveService.Progress.PlayerData.Room;
            _currentRoom = _objectsProvider.Rooms[room - 1];
            _currentRoom.gameObject.SetActive(true);
        }

        private void MoveCameraToPlayer()
        {
            CinemachineVirtualCamera vCam = _objectsProvider.VirtualCamera;
            Camera mCam = _objectsProvider.MainCamera;
            Vector3 playerPosition = _objectsProvider.Character.transform.position;

            vCam.enabled = false;
            mCam.enabled = false;
            vCam.transform.position = playerPosition;
            mCam.transform.position = playerPosition;
            
            vCam.enabled = true;
            mCam.enabled = true;
        }

        private void SpawnPlayer()
        {
            Transform character = _objectsProvider.Character.transform;
            character.position = _currentRoom.EntryPoint.position;
            character.localPosition += new Vector3(0, 0.3f, 0);
            character.gameObject.SetActive(true);

            CinemachineVirtualCamera virtualCamera = _objectsProvider.VirtualCamera;
            virtualCamera.LookAt = character;
            virtualCamera.Follow = character;
        }

        private void SpawnEnemies()
        {
            EnemySpawn[] enemySpawns = _currentRoom.EnemySpawns;
            var aliveEnemies = new List<Transform>(enemySpawns.Length);
            foreach (EnemySpawn enemySpawn in enemySpawns)
            {
                GameObject enemy = _enemyFactory.Create(enemySpawn);
                aliveEnemies.Add(enemy.transform);
            }

            _objectsProvider.AliveEnemies = aliveEnemies;
        }

        private void ShowDependentObjects()
        {
            _objectsProvider.Hud.gameObject.SetActive(true);
        }

        private void InitDependentServices()
        {
            _enemiesDeathObserver.AllCleared += EnterRoomCleared;
            _enemiesDeathObserver.Init();
        }

        private void SwitchToRoomMusic() 
            => _musicService.Play(_currentRoom.BackgroundMusic);
    }
}