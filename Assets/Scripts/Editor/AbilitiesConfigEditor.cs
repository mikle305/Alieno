using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GamePlay.Abilities;
using StaticData.Abilities;
using TriInspector.Editors;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(AbilitiesConfig))]
    public class AbilitiesConfigEditor : TriEditor
    {
        private static AbilitiesConfig _abilitiesConfig;
        private static Type[] _allAbilitiesTypes;


        [DidReloadScripts]
        private static void OnRecompile() 
            => UpdateConfig();

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Forced update"))
                UpdateConfig();
        }

        private static void UpdateConfig()
        {
            InitConfig();
            RemoveBrokenAbilities();
            CacheAbilitiesTypes();
            UpdateAbilitiesCollection();
            UpdateAbilitiesLevelsIds();
            EditorUtils.SaveSerialization(_abilitiesConfig);
        }

        private static void UpdateAbilitiesLevelsIds()
        {
            foreach (AbilityData abilityData in _abilitiesConfig.AbilitiesData)
            {
                var levels = abilityData
                    ?.GetType()
                    .GetProperty(nameof(AbilityData<AbilityLevelData>.Levels))
                    ?.GetValue(abilityData) as AbilityLevelData[];

                PropertyInfo idProperty = typeof(AbilityLevelData).GetProperty(nameof(AbilityLevelData.Id));
                for (var i = 0; i < levels!.Length; i++)
                {
                    AbilityLevelData level = levels![i];
                    idProperty!.SetValue(level, i + 1);
                }
            }
        }

        private static void RemoveBrokenAbilities()
        {
            List<AbilityData> abilitiesData = _abilitiesConfig.AbilitiesData;
            for (var i = 0; i < abilitiesData.Count; i++)
            {
                if (abilitiesData[i] != null)
                    continue;

                abilitiesData.RemoveAt(i);
            }
        }

        private static void CacheAbilitiesTypes()
        {
            _allAbilitiesTypes = TypeCache
                .GetTypesDerivedFrom<AbilityData>()
                .Where(t => !t.IsAbstract)
                .ToArray();
        }

        private static void InitConfig()
        {
            _abilitiesConfig = EditorUtils
                .GetSoInstances<AbilitiesConfig>()
                .Single();
        }

        private static void UpdateAbilitiesCollection()
        {
            AbilityData[] newAbilities = GetNewAbilities();
            if (newAbilities.Length == 0)
                return;

            SetNewAbilities(newAbilities);
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