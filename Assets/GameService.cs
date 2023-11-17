using System;
using System.Collections;
using System.Collections.Generic;
using Additional.Game;
using UnityEngine;

public class GameService : MonoSingleton<GameService>
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _cursor;
    [SerializeField] private GameObject _levelMap;
    [SerializeField] private List<GameObject> _roomList;
    
    private LevelMapService _levelMapService;

    private int _currentRoomIndex = -1;
    private GameObject _currentRoomObject;
    private RoomController _currentRoomController;

    private void Start()
    {
        _levelMapService = LevelMapService.Instance;
        _levelMapService.OnAnimationFinish += StartRoom;
        StartGame();
    }

    public void StartGame()
    {
        ToggleRoomObjects(false);
        ToggleMap(true);
    }

    private void StartRoom()
    {
        ToggleMap(false);
        ToggleRoomObjects(true);
        _currentRoomObject = Instantiate(_roomList[++_currentRoomIndex]);
        _currentRoomController = _currentRoomObject.GetComponent<RoomController>();
        
        _currentRoomController.MovePlayerToSpawnPoint(_player.transform);
        _currentRoomController.AddFinishEvent(FinishRoom);
    }


    
    private void FinishRoom()
    {
        ToggleRoomObjects(false);
        ToggleMap(true);
        
        Destroy(_currentRoomObject);
    }
    
    private void ToggleRoomObjects(bool value)
    {
        _player.SetActive(value);
        _cursor.SetActive(value);
    }

    private void ToggleMap(bool value)
    {
        _levelMap.SetActive(value);
    }
}