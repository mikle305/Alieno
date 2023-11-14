using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMapService : MonoBehaviour
{
    [SerializeField] private Button _nextLvlButton;
    [SerializeField] private Transform _pointer;
    [SerializeField] private List<Transform> _levelNumbers;
    [SerializeField] private Vector3 _offset = new Vector3(0,1,0);
    [SerializeField] private float _speed =1f;

    private int _currentRoom = 0;
    private int _totalRooms;
    private void Start()
    {
        _nextLvlButton.onClick.AddListener(DisplayNextRoom);
        _totalRooms = _levelNumbers.Count;
        StartCoroutine(LerpPointer(_levelNumbers[_currentRoom]));
    }

    public void DisplayNextRoom()
    {
        if(++_currentRoom >= _totalRooms)
            return;
        
        StartCoroutine(LerpPointer(_levelNumbers[_currentRoom]));
    }

    private IEnumerator LerpPointer(Transform moveTo)
    {
        Vector3 posToMove = moveTo.position + _offset;
        print("into");
        print(posToMove);
        print(_pointer.position);
        while ((Vector3.Distance(_pointer.position, posToMove) > 0.001f))
        {
            print("cycle");
            var step =  _speed * Time.deltaTime; // calculate distance to move
            _pointer.position = Vector3.MoveTowards(_pointer.position, posToMove, step);
            print(posToMove);
            print(_pointer.position);
            if (Vector3.Distance(_pointer.position, moveTo.position) < 0.001f)
            {
                moveTo.position *= -1.0f;
            } 
            
            yield return null;
        }
    }
}
