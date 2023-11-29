using System.Collections;
using Cysharp.Threading.Tasks;
using GamePlay.Abilities;
using GamePlay.Characteristics;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private AbilitiesEntity _abilitiesEntity;
    [SerializeField] private ProjectileAttackData _attackData;

    public bool OnCooldown { get; set; }
    
    public void Attack()
    {
        if(OnCooldown)
            return;

        _abilitiesEntity.CallShot();
        StartCooldown().Forget();
    }

    public void AttackWithDelay(float _attackDelay)
    {
        if(OnCooldown)
            return;
        
        StartCoroutine(WaitAndAttack(_attackDelay));
    }

    private IEnumerator WaitAndAttack(float _attackDelay)
    {
        StartCooldown().Forget();

        yield return new WaitForSeconds(_attackDelay);
        
        _abilitiesEntity.CallShot();

    }
    private async UniTask StartCooldown()
    {
        OnCooldown = true;
        await UniTask.WaitForSeconds(_attackData.AttackRate.GetValue());
        OnCooldown = false;
    }
}