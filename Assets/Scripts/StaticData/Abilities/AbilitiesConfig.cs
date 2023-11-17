using System.Collections.Generic;
using TriInspector;
using UnityEngine;

namespace StaticData.Abilities
{
    [CreateAssetMenu(menuName = "StaticData/AbilitiesConfig", fileName = "AbilitiesConfig")]
    public class AbilitiesConfig : ScriptableObject
    {
        [field: SerializeReference, HideReferencePicker] 
        public List<AbilityData> AbilitiesData { get; private set; } = new();
    }
}