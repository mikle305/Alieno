using System;
using Services;
using UnityEngine;

namespace GamePlay.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _body;
    
        private float _horizontal;
        private float _vertical;
        private PlayerDash _dashComponent;
        public float runSpeed = 5.0f;

        private void Awake()
        {
            _dashComponent = GetComponent<PlayerDash>();
        }

        private void Start()
        {
            GameService.Instance.OnRoomFinish += ClearVelocity;
        }

        void Update ()
        {
            _horizontal = Input.GetAxisRaw("Horizontal");
            _vertical = Input.GetAxisRaw("Vertical"); 
        }

        private void FixedUpdate()
        {  
            if(!_dashComponent.IsDashing)
                _body.velocity = new Vector3(_horizontal * runSpeed, 0,_vertical * runSpeed);
        }

        public void ClearVelocity()
        {
            _body.velocity = Vector3.zero;
        }
    }
}
