using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class ExitButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void Start()
        {
            _button.onClick.AddListener(ExitApplication);
        }

        private void ExitApplication() 
            => Application.Quit();
    }
}
