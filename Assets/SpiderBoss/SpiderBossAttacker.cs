using Cysharp.Threading.Tasks;
using GamePlay.Abilities;
using GamePlay.Characteristics;
using UnityEngine;

public class SpiderBossAttacker : MonoBehaviour
{
    [SerializeField] private SpiderBossAnimations _animations;
    [SerializeField] private AbilitiesEntity _laserEntity;
    [SerializeField] private AbilitiesEntity _jumpEntity;
    [SerializeField] private ProjectileAttackData _laserAttackData;
    [SerializeField] private ProjectileAttackData _jumpAttackData;
    
    private void Start()
    {
        _animations.JumpAnimationFinished += JumpAttack;
        _animations.AttackAnimationFinished += LaserAttack;
    }

    public void JumpAttack()
    {
        Attack(_jumpEntity, _jumpAttackData);
    }
    
    public void LaserAttack()
    {
        Attack(_laserEntity, _laserAttackData);
    }
    
    public void Attack(AbilitiesEntity abilitiesEntity,ProjectileAttackData attackData)
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
