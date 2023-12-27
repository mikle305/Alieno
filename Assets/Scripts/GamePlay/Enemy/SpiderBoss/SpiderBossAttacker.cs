using Cysharp.Threading.Tasks;
using GamePlay.Abilities;
using GamePlay.Characteristics;
using Services;
using UnityEngine;
using VContainer;

namespace GamePlay.Enemy
{
    public class SpiderBossAttacker : MonoBehaviour
    {
        [SerializeField] private SpiderBossAnimations _animations;
        [SerializeField] private AbilitiesEntity _laserEntity;
        [SerializeField] private AbilitiesEntity _jumpEntity;
        [SerializeField] private ProjectileAttackData _laserAttackData;
        [SerializeField] private ProjectileAttackData _jumpAttackData;
        [SerializeField] private GameObject _hpCanvas;
    
        private EndGameMenuService _endGameMenuService;

        
        [Inject]
        public void Construct(EndGameMenuService endGameMenuService)
        {
            _endGameMenuService = endGameMenuService;
        }
        
        private void Start()
        {
            _animations.JumpAnimationFinished += JumpAttack;
            _animations.AttackAnimationFinished += LaserAttack;
            _endGameMenuService.DefeatReceived += OnPlayerDied;
        }

        private void OnPlayerDied()
        {
            _endGameMenuService.DefeatReceived -= OnPlayerDied;
            _animations.JumpAnimationFinished -= JumpAttack;
            _animations.AttackAnimationFinished -= LaserAttack;
            _hpCanvas.SetActive(false);
        }

        private void JumpAttack()
        {
            Attack(_jumpEntity, _jumpAttackData);
        }

        private void LaserAttack()
        {
            Attack(_laserEntity, _laserAttackData);
        }

        private void Attack(AbilitiesEntity abilitiesEntity,ProjectileAttackData attackData)
        {
            abilitiesEntity.CallShot();
            StartCooldown(attackData).Forget();
        }
    
        private async UniTask StartCooldown(ProjectileAttackData attackData)
        {
            // OnCooldown = true;
            await UniTask.WaitForSeconds(attackData.AttackRate.GetValue());
            // OnCooldown = false;
        }
    }
}
