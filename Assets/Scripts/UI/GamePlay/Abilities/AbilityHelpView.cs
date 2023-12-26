using Services;
using StaticData;
using StaticData.Abilities;
using StaticData.UI;
using UnityEngine;

namespace UI.GamePlay
{
    public class AbilityHelpView : MonoBehaviour
    {
        [SerializeField] private RectTransform _gridContent;
        
        private StaticDataService _staticDataService;


        private void Start()
        {
            _staticDataService = StaticDataService.Instance;
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