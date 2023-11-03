using Additional.Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.WindowsInfo
{
    public class EmailLoginInfo : MonoSingleton<EmailLoginInfo>
    {
        [SerializeField] private TMP_InputField _emailField;
        [SerializeField] private TMP_InputField _passwordField;
        [SerializeField] private Toggle _rememberToggle;


        public string Email => _emailField.text;
        public string Password => _passwordField.text;
        public bool RememberMe => _rememberToggle.isOn;
    }
}