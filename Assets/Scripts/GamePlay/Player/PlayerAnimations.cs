using System;
using UnityEngine;

namespace GamePlay.Player
{
    public class PlayerAnimations : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private static readonly int _idle = Animator.StringToHash("Idle");
        private static readonly int _rightAttack = Animator.StringToHash("RightAttack");
        private static readonly int _leftAttack = Animator.StringToHash("LeftAttack");
        private static readonly int _attackSpeed = Animator.StringToHash("AttackSpeed");

        private int _lastAttackUsed = _leftAttack;
    
        public event Action OnAttackAnimation; 
    
    
        public void PlayAttackAnimation(float attackSpeed)
        {
            _animator.SetFloat(_attackSpeed,1/attackSpeed);
        
            int nextAttack = GetNextAttack();
            _animator.SetTrigger(nextAttack);
            _lastAttackUsed = nextAttack;
        }
    
        /// <summary>
        /// Animation event
        /// </summary>
        [JetBrains.Annotations.UsedImplicitly]
        public void InvokeAttack()
        {
            OnAttackAnimation?.Invoke();
        }

        private int GetNextAttack() 
            => _lastAttackUsed == _leftAttack 
                ? _rightAttack 
                : _leftAttack;
    }
}
