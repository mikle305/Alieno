using System;
using System.Collections;
using System.Collections.Generic;
using Additional.Game;
using UnityEngine;
using UnityEngine.UI;

namespace Services
{
    public class LevelMapService : MonoSingleton<LevelMapService>
    {
        [SerializeField] public Button _nextLvlButton;
        [SerializeField] private Transform _pointer;
        [SerializeField] private List<Transform> _levelNumbers;
        [SerializeField] private Vector3 _offset = new Vector3(0,1,0);
        [SerializeField] private float _speed =1f;
        [SerializeField] private Coroutine _animation;
        public event Action OnAnimationFinish;
        private int _currentRoom = -1;
        private int _totalRooms;
        private Vector3 _positionToMove;
        private void Start()
        {
            _nextLvlButton.onClick.AddListener(DisplayNextRoom);
            _totalRooms = _levelNumbers.Count;
        }

        public void DisplayNextRoom()
        {
            if (_animation != null)
            {
                SkipAnimation();
                return;
            }
        
            if(++_currentRoom >= _totalRooms)
                return;

            if (_animation == null)
                _animation = StartCoroutine(MoveAnimation(_levelNumbers[_currentRoom]));
            else
                SkipAnimation();
        }

        private IEnumerator MoveAnimation(Transform moveTo)
        {
            _positionToMove = moveTo.position + _offset;
            while ((Vector3.Distance(_pointer.position, _positionToMove) > 0.001f))
            {
                var step =  _speed * Time.deltaTime; // calculate distance to move
                _pointer.position = Vector3.MoveTowards(_pointer.position, _positionToMove, step);
                if (Vector3.Distance(_pointer.position, moveTo.position) < 0.001f)
                {
                    moveTo.position *= -1.0f;
                } 
            
                yield return null;
            }

            yield return new WaitForSeconds(1f);
     
            _animation = null;
            OnAnimationFinish?.Invoke();
        }

        private void SkipAnimation()
        {
            StopCoroutine(_animation);
            _animation = null;

            _pointer.position = _positionToMove;

            OnAnimationFinish?.Invoke();
        }
    }
}
