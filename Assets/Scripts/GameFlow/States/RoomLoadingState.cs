using System.Collections.Generic;
using GameFlow.Context;
using GamePlay.Enemy;
using GamePlay.Other;
using Services;
using Services.Save;
using UnityEngine;

namespace GameFlow.States
{
    public class RoomLoadingState : State
    {
        private readonly GameStateMachine _context;
        private readonly SaveService _saveService;
        private readonly ObjectsProvider _objectsProvider;
        private Room _currentRoom;
        private EnemyFactory _enemyFactory;


        public RoomLoadingState(GameStateMachine context)
        {
            _context = context;
            _saveService = SaveService.Instance;
            _objectsProvider = ObjectsProvider.Instance;
            _enemyFactory = EnemyFactory.Instance;
        }

        public override void Enter()
        {
            InitCurrentRoom();
            SpawnPlayer();
            SpawnEnemies();
        }

        public override void Exit()
        {
            
        }

        private void InitCurrentRoom()
        {
            int room = _saveService.Progress.PlayerData.Room;
            _currentRoom = _objectsProvider.Rooms[room];
            _currentRoom.gameObject.SetActive(true);
        }

        private void SpawnPlayer()
        {
            Transform character = _objectsProvider.Character.transform;
            character.position = _currentRoom.EntryPoint.position;
            character.localPosition += new Vector3(0, 0.3f, 0);
            character.gameObject.SetActive(true);
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
    }
}