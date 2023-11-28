using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotation : MonoBehaviour
{
    [SerializeField] private Transform _enemyTransform;
    [SerializeField] private float _turnSpeed = 3f;

    public void UpdateRotation(Transform _target)
    {
        Quaternion OriginalRot = _enemyTransform.rotation;
        _enemyTransform.LookAt(_target);
        
        Quaternion NewRot = transform.rotation;
        
        transform.rotation = Quaternion.Lerp(OriginalRot, NewRot, _turnSpeed * Time.deltaTime);
    }
}
