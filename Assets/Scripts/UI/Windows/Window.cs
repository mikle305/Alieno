using System;
using UnityEngine;

namespace UI.Windows
{
    public abstract class Window : MonoBehaviour
    {
        [SerializeField] private GameObject _background;
        
        private bool _isShown;
        

        public void Toggle(ToggleMode toggleMode = ToggleMode.Toggle, Action onStart = null, Action onDone = null)
        {
            switch (toggleMode)
            {
                case ToggleMode.Toggle:
                    OnToggle(onStart, onDone);
                    break;
                case ToggleMode.Open:
                    OnShow();
                    break;
                case ToggleMode.Close:
                    OnHide();
                    break;
            }
        }
        
        protected abstract void Show(Action onStart = null, Action onDone = null);
        
        protected abstract void Hide(Action onStart = null, Action onDone = null);
        
        
        private void OnToggle(Action onStart, Action onDone)
        {
            if( _isShown )
                Hide(onStart, onDone);
            else
                Show(onStart, onDone);
            _isShown = !_isShown;
        }

        private void OnShow()
        {
            Show();
            _isShown = true;
            if (_background != null)
                _background.SetActive(true);
        }
        
        private void OnHide()
        {
            Hide();
            _isShown = false;
            if (_background != null)
                _background.SetActive(false);
        }
    }
}