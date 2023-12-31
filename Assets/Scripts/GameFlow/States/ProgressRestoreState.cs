﻿using System.Collections.Generic;
using Additional.Constants;
using GameFlow.Context;
using GamePlay.Abilities;
using GamePlay.Characteristics;
using Services;
using Services.Factories;
using Services.Save;
using UI.GamePlay;
using UnityEngine;

namespace GameFlow.States
{
    public class ProgressRestoreState : State
    {
        private readonly GameStateMachine _context;
        private readonly SaveService _saveService;
        private readonly ObjectsProvider _objectsProvider;
        private HudFactory _hudFactory;


        public ProgressRestoreState(GameStateMachine context)
        {
            _context = context;
            _saveService = SaveService.Instance;
            _objectsProvider = ObjectsProvider.Instance;
        }

        public override void Enter()
        {
            RestorePlayerHealth();
            RestorePlayerAbilities();
            InitHud();
            
            if (IsAbilityNotSelected())
                EnterLastRoomCheck();
            else
                EnterRoomSelection();
        }

        private void RestorePlayerHealth()
        {
            var healthData = _objectsProvider.Character.GetComponent<HealthData>();
            float restoredHealth = _saveService.Progress.PlayerData.CurrentHealth;
            healthData.Init(restoredHealth, DefaultPlayerProgress.Health);
        }

        private void RestorePlayerAbilities()
        {
            var abilitiesEntity = _objectsProvider.Character.GetComponent<AbilitiesEntity>();
            Dictionary<AbilityId,int> restoredAbilities = _saveService.Progress.PlayerData.CurrentAbilities;

            foreach (KeyValuePair<AbilityId,int> restoredAbility in restoredAbilities)
                abilitiesEntity.AddAbility(restoredAbility.Key, restoredAbility.Value);
        }

        private bool IsAbilityNotSelected() 
            => !_saveService.Progress.PlayerData.AbilitySelected;

        private void EnterRoomSelection()
            => _context.Enter<RoomSelectionState>();

        private void EnterLastRoomCheck()
            => _context.Enter<LastRoomCheckState>();

        private void InitHud()
        {
            GameObject character = _objectsProvider.Character;
            _hudFactory = HudFactory.Instance;
            Hud hud = _hudFactory.Create(character);
            hud.gameObject.SetActive(false);
            _objectsProvider.Hud = hud;
        }
    }
}