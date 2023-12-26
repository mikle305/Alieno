using System;
using System.Collections;
using Additional.Game;
using StaticData.GameConfig;
using UnityEngine;
using VContainer;

namespace Services
{
    public class LevelMapService
    {
        private int _currentRoom = -1;
        private Vector3 _positionToMove;
        private Coroutine _animation;
        private readonly ObjectsProvider _objectsProvider;
        private readonly LevelMapData _levelMapData;
        private readonly ICoroutineRunner _coroutineRunner;

        public event Action AnimationFinished;

        
        private LevelMapService(ObjectsProvider objectsProvider, StaticDataService staticDataService, ICoroutineRunner coroutineRunner)
        {
            _objectsProvider = objectsProvider;
            _coroutineRunner = coroutineRunner;
            _levelMapData = staticDataService.GetGamePlayConfig().LevelMapData;
        }

        public void Init(int currentRoom)
        {
            _currentRoom = currentRoom;
            if (_currentRoom != -1)
            {
                _positionToMove = _objectsProvider.RoomsMap.LevelNumbers[_currentRoom].position + _levelMapData.Offset;
                _objectsProvider.RoomsMap.Pointer.position = _positionToMove;
            }
            
            if (CheckForAutoSkip())
            {
                AnimationFinished?.Invoke();
                return;
            }
            
            _objectsProvider.RoomsMap.gameObject.SetActive(true);
            _objectsProvider.RoomsMap.NextLvlButton.onClick.AddListener(DisplayNextRoom);

        }
        public void DisplayNextRoom()
        {
            if (_animation != null)
            {
                SkipAnimation();
                return;
            }
            
            if(++_currentRoom >= _objectsProvider.RoomsMap.LevelNumbers.Count)
                return;

            if (_animation == null)
                _animation = _coroutineRunner.StartCoroutine(MoveAnimation(_objectsProvider.RoomsMap.LevelNumbers[_currentRoom]));
            else
                SkipAnimation();
        }

        private bool CheckForAutoSkip() 
            => _objectsProvider.RoomsMap.AutoSkipToggle.isOn;

        private IEnumerator MoveAnimation(Transform moveTo)
        {
            _positionToMove = moveTo.position + _levelMapData.Offset;
            while ((Vector3.Distance(_objectsProvider.RoomsMap.Pointer.position, _positionToMove) > 0.001f))
            {
                var step =  _levelMapData.Speed * Time.deltaTime; // calculate distance to move
                _objectsProvider.RoomsMap.Pointer.position = Vector3.MoveTowards(_objectsProvider.RoomsMap.Pointer.position, _positionToMove, step);
                if (Vector3.Distance(_objectsProvider.RoomsMap.Pointer.position, moveTo.position) < 0.001f)
                {
                    moveTo.position *= -1.0f;
                } 
            
                yield return null;
            }

            yield return new WaitForSeconds(1f);
     
            _animation = null;
            AnimationFinished?.Invoke();
        }

        public void SkipAnimation()
        {
            _coroutineRunner.StopCoroutine(_animation);
            _animation = null;

            _objectsProvider.RoomsMap.Pointer.position = _positionToMove;
            AnimationFinished?.Invoke();
        }
    }
}
