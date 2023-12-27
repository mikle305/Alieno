using Services;
using UI.Windows;
using UnityEngine;
using VContainer;

namespace UI.Menu
{
    public class DefeatMenuView : MonoBehaviour
    {
        [SerializeField] private Window _window;
        
        private EndGameMenuService _endGameMenuService;

        
        [Inject]
        public void Construct(EndGameMenuService endGameMenuService)
        {
            _endGameMenuService = endGameMenuService;
            _endGameMenuService.DefeatReceived += ShowWindow;
        }

        private void OnDestroy()
        {
            _endGameMenuService.DefeatReceived -= ShowWindow;
        }

        private void ShowWindow() 
            => _window.Toggle(ToggleMode.Open);
    }
}