using System;
using System.Collections.Generic;
using System.Linq;
using StaticData.Abilities;
using TriInspector.Editors;
using UnityEditor;
using UnityEditor.Callbacks;

namespace Editor
{
    [CustomEditor(typeof(AbilitiesConfig))]
    public class AbilitiesConfigEditor : TriEditor
    {
        private static AbilitiesConfig _abilitiesConfig;
        private static Type[] _allAbilitiesTypes;
        

        [DidReloadScripts]
        private static void OnRecompile()
        {
            InitConfig();
            RemoveBrokenAbilities();
            CacheAllTypes();
            UpdateAbilities();
        }

        private static void RemoveBrokenAbilities()
        {
            List<AbilityData> abilitiesData = _abilitiesConfig.AbilitiesData;
            var hasBroken = false;
            for(var i = 0; i < abilitiesData.Count; i++)
            {
                if(abilitiesData[i] != null)
                    continue;
                
                abilitiesData.RemoveAt(i);
                hasBroken = true;
            }
            
            if (hasBroken)
                EditorUtils.SaveSerialization(_abilitiesConfig);
        }

        private static void CacheAllTypes()
        {
            _allAbilitiesTypes = TypeCache
                .GetTypesDerivedFrom<AbilityData>()
                .ToArray();
        }

        private static void InitConfig()
        {
            _abilitiesConfig = EditorUtils
                .GetSoInstances<AbilitiesConfig>()
                .Single();
        }

        private static void UpdateAbilities()
        {
            AbilityData[] newAbilities = GetNewAbilities();
            if (newAbilities.Length == 0)
                return;

            SetNewAbilities(newAbilities);
            EditorUtils.SaveSerialization(_abilitiesConfig);
        }

        private static void SetNewAbilities(AbilityData[] newAbilities)
            => _abilitiesConfig.AbilitiesData.AddRange(newAbilities);

        private static AbilityData[] GetNewAbilities()
            => _allAbilitiesTypes
                .Where(IsNotInConfig)
                .Select(abilityType => Activator.CreateInstance(abilityType) as AbilityData)
                .ToArray();
        
        private static bool IsNotInConfig(Type abilityType)
            => !_abilitiesConfig.AbilitiesData.Exists(a => a.GetType() == abilityType);
    }
}