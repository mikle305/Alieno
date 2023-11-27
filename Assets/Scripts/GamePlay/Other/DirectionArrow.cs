using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionArrow : MonoBehaviour
{
    [SerializeField] private Transform _exitPoint;
    private void Update()
    {
        if (_exitPoint == null)
        {
            TryFindExit();
            return;
        }
        
        transform.LookAt(_exitPoint);
    }

    private void TryFindExit()
    {
        _exitPoint = GameObject.Find("ExitPoint").transform;
    }
}
