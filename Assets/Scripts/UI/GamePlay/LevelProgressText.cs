﻿using SaveData;
using Services;
using TMPro;
using UnityEngine;

namespace UI.GamePlay
{
    public class LevelProgressText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        private MainMenuService _menuService;


        private void Awake()
        {
            _menuService = MainMenuService.Instance;
            _menuService.ProgressReceived += DisplayLevel;
        }

        private void DisplayLevel(PlayerData playerProgress) 
            => _text.text = $"{playerProgress.Level}-{playerProgress.Room}";
    }
}