using GamePlay.Abilities;
using Services;
using StaticData.Abilities;
using UnityEngine;

namespace GamePlay.Player
{
    public class PlayerAbilities : MonoBehaviour
    {
        private StaticDataService _staticDataService;
        
        
        private void Start()
        {
            _staticDataService = StaticDataService.Instance;
        }
        
        public void AddAbility(AbilityId abilityId)
        {
            AbilityData abilityData = _staticDataService.GetAbility(abilityId);
        }
    }
}