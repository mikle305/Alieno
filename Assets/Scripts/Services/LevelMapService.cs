using System;
using System.Collections;
using Additional.Game;
using UnityEngine;

namespace Services
{
    public class LevelMapService : MonoSingleton<LevelMapService>
    {
        [SerializeField] private Vector3 _offset = new(0,1,0);
        [SerializeField] private float _speed =1f;

        private int _currentRoom = -1;
        private Vector3 _positionToMove;
        private Coroutine _animation;
        private ObjectsProvider _objectsProvider;
        
        public event Action AnimationFinished;
        
        
        private void Start()
        {
            _objectsProvider = ObjectsProvider.Instance;
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
                _animation = StartCoroutine(MoveAnimation(_objectsProvider.RoomsMap.LevelNumbers[_currentRoom]));
            else
                SkipAnimation();
        }

        private IEnumerator MoveAnimation(Transform moveTo)
        {
            _positionToMove = moveTo.position + _offset;
            while ((Vector3.Distance(_objectsProvider.RoomsMap.Pointer.position, _positionToMove) > 0.001f))
            {
                var step =  _speed * Time.deltaTime; // calculate distance to move
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

        private void SkipAnimation()
        {
            StopCoroutine(_animation);
            _animation = null;

            _objectsProvider.RoomsMap.Pointer.position = _positionToMove;
            AnimationFinished?.Invoke();
        }
    }
}
