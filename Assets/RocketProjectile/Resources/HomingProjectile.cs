using System;
using System.Collections;
using System.Collections.Generic;
using Services;
using UnityEngine;
using UnityEngine.AI;

public class HomingProjectile : MonoBehaviour
{
    // TemporaryDisabled
    
    // private GameObject _player;
    // [SerializeField] private float _turnSpeed = 15f;
    // [SerializeField] private Rigidbody _projRigid;
    // private void Start()
    // {
    //     if (_player == null)
    //         _player = ObjectsProvider.Instance.Character;
    // }
    //
    // private void OnEnable()
    // {
    //     if (_player == null)
    //         _player = ObjectsProvider.Instance.Character;
    // }
    //
    // private void FixedUpdate()
    // {
    //     RotateToPlayer(_player.transform);
    // }
    //
    // private void RotateToPlayer(Transform target)
    // {
    //     Quaternion deltaQuat = Quaternion.FromToRotation(_projRigid.transform.forward, target.position);
    //
    //     Vector3 axis;
    //     float angle;
    //     deltaQuat.ToAngleAxis(out angle, out axis);
    //
    //     float dampenFactor = 0.8f; // this value requires tuning
    //     _projRigid.AddTorque(-_projRigid.angularVelocity * dampenFactor, ForceMode.Acceleration);
    //
    //     float adjustFactor = 45.5f; // this value requires tuning
    //     _projRigid.AddTorque(axis.normalized * angle * adjustFactor, ForceMode.Acceleration);
    // }
}
