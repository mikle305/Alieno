using System;
using System.Collections.Generic;
using Additional.Extensions;
using Additional.Utils;
using Services;
using UnityEngine;

namespace GamePlay.Abilities
{
    public class AbilitiesEntity : MonoBehaviour
    {
        private StaticDataService _staticDataService;
        private readonly Dictionary<AbilityId, AbilityComponent> _abilitiesMap = new();

        public IReadOnlyDictionary<AbilityId, AbilityComponent> AbilitiesMap => _abilitiesMap;
        
        
        private void Start()
        {
            _staticDataService = StaticDataService.Instance;
        }

        private void Update()
            => _abilitiesMap.ForEach(item => item.Value.OnTick());

        public void Call() 
            => _abilitiesMap.ForEach(item => item.Value.OnCall());

        public void AddAbility(AbilityId abilityId)
        {
            if (_abilitiesMap.ContainsKey(abilityId))
                ThrowUtils.ComponentAlreadyAdded();

            AbilityComponent ability = CreateAbility(abilityId);
            if (ability == null)
                return;
            
            _abilitiesMap[abilityId] = ability;
        }

        public void UpLevel(AbilityId abilityId)
        {
            if (!_abilitiesMap.TryGetValue(abilityId, out AbilityComponent ability))
                ThrowUtils.ComponentNotAdded();
            
            ability!.UpLevel();
        }

        public void RemoveAbility(AbilityId abilityId)
        {
            if (!_abilitiesMap.Remove(abilityId, out AbilityComponent ability))
                ThrowUtils.ComponentNotAdded();
            
            ability.OnDestroy();
        }

        private AbilityComponent CreateAbility(AbilityId abilityId)
        {
            if (_staticDataService == null)
                return null;
            
            AbilityData abilityData = _staticDataService.GetAbility(abilityId);
            var abilityComponent = Activator.CreateInstance(abilityData.ComponentType) as AbilityComponent;
            abilityComponent!.Init(this, abilityData);
            return abilityComponent;
        }
    }
}