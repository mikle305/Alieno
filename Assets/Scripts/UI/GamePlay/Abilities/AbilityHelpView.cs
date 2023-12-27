using Services;
using StaticData.Abilities;
using StaticData.UI;
using UnityEngine;
using VContainer;

namespace UI.GamePlay
{
    public class AbilityHelpView : MonoBehaviour
    {
        [SerializeField] private RectTransform _gridContent;
        
        private StaticDataService _staticDataService;

        
        [Inject]
        public void Construct(StaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }
        
        private void Start()
        {
            InitAllElements();
        }

        private void InitAllElements()
        {
            AbilityHelpElement elementPrefab = _staticDataService.GetPrefabsConfig().AbilityHelpElement;
            PlayerAbilityData[] playerAbilities = _staticDataService.GetAbilitiesConfig().PlayerAbilitiesData;
            UiConfig uiConfig = _staticDataService.GetUiConfig();
            
            foreach (PlayerAbilityData abilityData in playerAbilities)
            {
                AbilityUiData abilityUiData = uiConfig.GetAbilityData(abilityData.AbilityId);
                AbilityHelpElement helpElement = Instantiate(elementPrefab, _gridContent);
                helpElement.Icon.sprite = abilityUiData.Icon;
                helpElement.TextMesh.text = $"{abilityUiData.Name}, max level {abilityData.Levels[^1]}\n{abilityUiData.Description}";
            }
        }
    }
}