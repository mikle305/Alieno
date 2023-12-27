using Services;
using UnityEngine;
using VContainer;

namespace GamePlay.Player
{
    public class PlayerState : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerRotation _rotation;
        [SerializeField] private PlayerAttacker _attacker;
        [SerializeField] private PlayerDash _dash;
        
        private IInputService _inputService;
        private bool _dashInvokedInput;
        private Vector2 _moveDirectionInput;

        
        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Update()
        {
            _moveDirectionInput = _inputService.GetMoveDirection();
            _dashInvokedInput = _inputService.IsDashInvoked() && !_dash.OnCooldown;
        }

        private void FixedUpdate()
        {
            if (_dash.IsDashing)
                return;

            _attacker.IsAutoAttacking = true;
            
            if (_moveDirectionInput == Vector2.zero)
            {
                _movement.Stop();
                return;
            }
            
            if (_dashInvokedInput)
            {
                _movement.Stop();
                _attacker.IsAutoAttacking = false;
                _dash.Dash(_moveDirectionInput);
                return;
            }

            _movement.Move(_moveDirectionInput);
            _rotation.Rotate(Time.fixedDeltaTime);
        }

        private void OnDisable()
        {
            _movement.Stop();
            _dash.Stop();
        }
    }
}