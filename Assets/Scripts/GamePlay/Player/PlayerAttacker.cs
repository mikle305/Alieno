using Cysharp.Threading.Tasks;
using GamePlay.Abilities;
using GamePlay.Characteristics;
using Services;
using UnityEngine;
using VContainer;

namespace GamePlay.Player
{
    public class PlayerAttacker : MonoBehaviour
    {
        [SerializeField] private AbilitiesEntity _abilitiesEntity;
        [SerializeField] private ProjectileAttackData _attackData;
        [SerializeField] private PlayerAnimations _animations;

        private bool _onCooldown;
        private RadarService _radarService;

        public bool IsAutoAttacking { get; set; }


        [Inject]
        public void Construct(RadarService radarService)
        {
            _radarService = radarService;
        }
        
        private void Start()
        {
            _animations.OnAttackAnimation += Attack;
        }

        private void Attack()
        {
            _abilitiesEntity.CallShot();
        }

        private void Update() 
            => AttackOnCooldown();

        private void AttackOnCooldown()
        {
            if (!IsAutoAttacking || _onCooldown) 
                return;

            Transform enemy = _radarService.GetClosestFromPlayer();
            if(enemy == null)
                return;
            
            _animations.PlayAttackAnimation(_attackData.AttackRate.GetValue());
            StartCooldown().Forget();
        }

        private async UniTask StartCooldown()
        {
            _onCooldown = true;
            await UniTask.WaitForSeconds(_attackData.AttackRate.GetValue());
            _onCooldown = false;
        }
    }
}