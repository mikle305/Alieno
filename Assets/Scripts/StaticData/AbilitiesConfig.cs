using System.Collections.Generic;
using GamePlay.Abilities;
using TriInspector;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(menuName = "StaticData/AbilitiesConfig", fileName = "AbilitiesConfig")]
    public class AbilitiesConfig : ScriptableObject
    {
        [field: SerializeReference, HideReferencePicker]
        public List<AbilityData> AbilitiesData { get; private set; } = new();
    }
}