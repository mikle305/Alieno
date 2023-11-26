using System;
using GamePlay.Damage;
using GamePlay.Projectile;
using Services;
using Services.Damage;

namespace GamePlay.Abilities
{
    public class StatusAbilityComponent<TData, TLevelData> : AbilityComponent<TData, TLevelData> 
        where TData : AbilityData<TLevelData> 
        where TLevelData : AbilityLevelData, new()
    {
        private StatusesMapper _statusesMapper;

        protected override void OnCreate() 
            => _statusesMapper = StatusesMapper.Instance;

        public override void OnShotDone(ProjectileDamage projectile) 
            => AddStatusToProjectile(projectile);

        private void AddStatusToProjectile(ProjectileDamage projectile)
        {
            Status status = _statusesMapper.Map(CurrentLevel, out Type statusType);
            projectile.Statuses[statusType] = status;
        }
    }
}