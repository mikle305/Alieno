using System.Collections.Generic;
using System.Linq;
using GamePlay.Abilities;
using TriInspector;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(menuName = "StaticData/Abilities Config", fileName = "AbilitiesConfig")]
    public class AbilitiesConfig : ScriptableObject
    {
        [field: SerializeReference, HideReferencePicker] public List<AbilityData> AbilitiesData { get; private set; } = new();
        [field: SerializeField] public PlayerAbilityData[] PlayerAbilitiesData { get; private set; }
        
        private Dictionary<AbilityId,AbilityData> _abilitiesDataMap;

        
        public AbilityData GetAbility(AbilityId abilityId)
            => (_abilitiesDataMap ??= CreateAbilitiesMap())[abilityId];
        
        private Dictionary<AbilityId, AbilityData> CreateAbilitiesMap() 
            => AbilitiesData.ToDictionary(a => a.Id, a => a);
    }
}