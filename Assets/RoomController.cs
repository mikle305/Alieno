using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] public GameObject _entryPoint;
    [SerializeField] public GameObject _exitPoint;
    
    public void MovePlayerToSpawnPoint(Transform player)
    {
        player.position = _entryPoint.transform.position;
        player.localPosition += new Vector3(0, 0.3f, 0);
    }

    public void AddFinishEvent(Action FinishRoom)
    {
        var detector = _exitPoint.AddComponent<PlayerCollisionDetector>();

        detector.InitCollisionTrigger(FinishRoom);
    }
    
    public void ToggleExitPoint(bool value)
    {
        _entryPoint.SetActive(value);
    }
}
