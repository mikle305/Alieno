using Services;
using UnityEngine;

namespace GamePlay.Player
{
    public class PlayerState : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerAttacker _attacker;
        [SerializeField] private PlayerDash _dash;
        
        private InputService _inputService;
        private GameService _gameService;
        
        private Vector2 _moveDirection;
        private bool _isDashInvoked;


        private void Start()
        {
            _gameService = GameService.Instance;
            _inputService = InputService.Instance;
            _gameService.OnRoomFinish += OnRoomChanged;
        }

        private void Update()
        {
            _moveDirection = _inputService.GetMoveDirection();
            _isDashInvoked = _inputService.IsDashInvoked() && !_dash.OnCooldown;
        }

        private void FixedUpdate()
        {
            if (_dash.IsDashing)
                return;

            _attacker.IsAutoAttacking = true;
            if (_moveDirection == Vector2.zero)
            {
                _movement.Stop();
                return;
            }
            
            if (_isDashInvoked)
            {
                _movement.Stop();
                _attacker.IsAutoAttacking = false;
                _dash.Dash(_moveDirection);
                return;
            }

            _movement.Move(_moveDirection);
        }

        private void OnRoomChanged()
        {
            _movement.Stop();
            _dash.Stop();
        }
    }
}