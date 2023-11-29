using System;
using System.Collections.Generic;
using Additional.Extensions;
using Additional.Utils;
using GamePlay.Projectile;
using Services;
using UnityEngine;

namespace GamePlay.Abilities
{
    public class AbilitiesEntity : MonoBehaviour
    {
        private StaticDataService _staticDataService;
        private readonly Dictionary<AbilityId, AbilityComponent> _abilitiesMap = new();

        public IReadOnlyDictionary<AbilityId, AbilityComponent> AbilitiesMap => _abilitiesMap;
        
        
        private void Awake()
        {
            _staticDataService = StaticDataService.Instance;
        }

        private void Update()
            => _abilitiesMap.ForEach(item => item.Value.OnTick());

        public void CallShot() 
            => _abilitiesMap.ForEach(item => item.Value.OnShotCalled());

        public void EndShot(ProjectileDamage projectile)
            => _abilitiesMap.ForEach(item => item.Value.OnShotDone(projectile));

        public void AddAbility(AbilityId abilityId, int level = 1)
        {
            if (_abilitiesMap.ContainsKey(abilityId))
                ThrowUtils.ComponentAlreadyAdded();

            AbilityComponent ability = CreateAbility(abilityId, level);
            if (ability == null)
                return;
            
            _abilitiesMap[abilityId] = ability;
        }

        public void SetLevel(AbilityId abilityId, int level)
        {
            if (!_abilitiesMap.TryGetValue(abilityId, out AbilityComponent ability))
                ThrowUtils.ComponentNotAdded();
            
            ability!.SetLevel(level);
        }

        public void RemoveAbility(AbilityId abilityId)
        {
            if (!_abilitiesMap.Remove(abilityId, out AbilityComponent ability))
                ThrowUtils.ComponentNotAdded();
            
            ability.OnDestroy();
        }

        private AbilityComponent CreateAbility(AbilityId abilityId, int level)
        {
            AbilityData abilityData = _staticDataService.GetAbilitiesConfig().GetAbility(abilityId);
            var abilityComponent = Activator.CreateInstance(abilityData.ComponentType) as AbilityComponent;
            abilityComponent!.Init(this, abilityData, level);
            return abilityComponent;
        }
    }
}