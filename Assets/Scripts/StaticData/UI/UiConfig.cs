using System.Collections.Generic;
using System.Linq;
using GamePlay.Abilities;
using UnityEngine;

namespace StaticData.UI
{
    [CreateAssetMenu(menuName = "StaticData/Ui Config", fileName = "UiConfig")]
    public class UiConfig : ScriptableObject
    {
        [SerializeField] private AbilityUiData[] _abilitiesData;
        
        private Dictionary<AbilityId,AbilityUiData> _abilitiesDataMap;


        public AbilityUiData GetAbilityData(AbilityId abilityId)
            => (_abilitiesDataMap ??= _abilitiesData.ToDictionary(a => a.Id, a => a))[abilityId];
    }
}