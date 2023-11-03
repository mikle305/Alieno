using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public class ToggleWindowButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Window _window;
        [SerializeField] private ToggleMode _toggleMode = ToggleMode.Toggle;
        

        private void Start()
            => _button.onClick.AddListener(ToggleWindow);

        private void ToggleWindow()
            => _window.Toggle(_toggleMode);
    }
}