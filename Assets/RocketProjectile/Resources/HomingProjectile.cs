using System;
using System.Collections;
using System.Collections.Generic;
using Services;
using UnityEngine;
using UnityEngine.AI;

public class HomingProjectile : MonoBehaviour
{

    private GameObject _player;
    [SerializeField] private float _turnSpeed = 15f;
    [SerializeField] private Rigidbody _projRigid;
    private void Start()
    {
        if (_player == null)
            _player = ObjectsProvider.Instance.Character;
    }
    
    private void OnEnable()
    {
        if (_player == null)
            _player = ObjectsProvider.Instance.Character;
    }
    
    private void FixedUpdate()
    {
        RotateToPlayer(_player.transform);
    }
    
    private void RotateToPlayer(Transform target)
    {
        // var impulse = _projRigid.Get();
        // // _projRigid.velocity = Vector3.zero;
        // print(impulse);
        Quaternion originalRotation = transform.rotation;
        transform.LookAt(target.position+new Vector3(0,0.3f,0));
        Quaternion newRotation = transform.rotation;

        transform.rotation = Quaternion.Lerp(originalRotation, newRotation, _turnSpeed * Time.deltaTime);
        _projRigid.AddForce(_projRigid.transform.forward*5.5f,ForceMode.Impulse);
    }
}
