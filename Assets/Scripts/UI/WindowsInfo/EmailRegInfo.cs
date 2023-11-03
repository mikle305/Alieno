using Additional.Game;
using TMPro;
using UnityEngine;

namespace UI.WindowsInfo
{
    public class EmailRegInfo : MonoSingleton<EmailRegInfo>
    {
        [SerializeField] private TMP_InputField _emailField;
        [SerializeField] private TMP_InputField _passwordField;
        [SerializeField] private TMP_InputField _usernameField;


        public string Email => _emailField.text;
        public string Password => _passwordField.text;
        public string Username => _usernameField.text;
    }
}