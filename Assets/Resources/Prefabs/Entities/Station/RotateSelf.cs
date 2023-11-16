using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSelf : MonoBehaviour
{
    [SerializeField] float _speed = 50f;

    void Update()
    {
        transform.Rotate (Vector3.up * _speed * Time.deltaTime, Space.Self);
    }
}
