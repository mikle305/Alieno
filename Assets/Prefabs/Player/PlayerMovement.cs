using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _body;
    
    private float _horizontal;
    private float _vertical;

    public float runSpeed = 5.0f;

    void Update ()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical"); 
    }

    private void FixedUpdate()
    {  
        _body.velocity = new Vector3(_horizontal * runSpeed, 0,_vertical * runSpeed);
    }
}
