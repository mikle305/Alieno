using Cysharp.Threading.Tasks;
using GamePlay.Abilities;
using GamePlay.Characteristics;
using UnityEngine;

namespace GamePlay.Player
{
    public class PlayerAttacker : MonoBehaviour
    {
        [SerializeField] private AbilitiesEntity _abilitiesEntity;
        [SerializeField] private ProjectileAttackData _attackData;
        [SerializeField] private PlayerAnimations _animations;

        private bool _onCooldown;

        public bool IsAutoAttacking { get; set; }

        private void Start()
        {
            _animations.OnAttackAnimation += Attack;
        }

        private void Attack()
        {
            _abilitiesEntity.Call();
        }

        private void Update() 
            => AttackOnCooldown();

        private void AttackOnCooldown()
        {
            if (!IsAutoAttacking || _onCooldown) 
                return;
            
            _animations.PlayAttackAnimation(_attackData.UseRate.GetValue());
            StartCooldown().Forget();
        }

        private async UniTask StartCooldown()
        {
            _onCooldown = true;
            await UniTask.WaitForSeconds(_attackData.UseRate.GetValue());
            _onCooldown = false;
        }
    }
}