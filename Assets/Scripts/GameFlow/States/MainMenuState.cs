﻿using Additional.Constants;
using GameFlow.Context;
using Services;
using StaticData.Music;

namespace GameFlow.States
{
    public class MainMenuState : State
    {
        private readonly GameStateMachine _context;
        private readonly SceneLoader _sceneLoader;
        private readonly MainMenuService _menuService;
        private readonly MusicService _musicService;


        public MainMenuState(GameStateMachine context)
        {
            _context = context;
            _sceneLoader = SceneLoader.Instance;
            _menuService = MainMenuService.Instance;
            _musicService = MusicService.Instance;
        }

        public override void Enter()
        {
            _sceneLoader.Load(SceneNames.MainMenu, OnMainMenuLoaded);
        }
        
        private void OnMainMenuLoaded()
        {
            _menuService.GameStarted += EnterSceneLoading;
            PlayMenuMusic();
        }

        public override void Exit()
        {
            _menuService.GameStarted -= EnterSceneLoading;
        }

        private void PlayMenuMusic() 
            => _musicService.Play(MusicId.MainMenu1);

        private void EnterSceneLoading() 
            => _context.Enter<SceneLoadingState>();
    }
}