using Services;
using UI.Windows;
using UnityEngine;

namespace UI.Menu
{
    public class DefeatMenuView : MonoBehaviour
    {
        [SerializeField] private Window _window;
        
        private EndGameMenuService _endGameMenuService;

        
        private void Awake()
        {
            _endGameMenuService = EndGameMenuService.Instance;
            _endGameMenuService.DefeatReceived += ShowWindow;
        }

        private void ShowWindow() 
            => _window.Toggle(ToggleMode.Open);
    }
}