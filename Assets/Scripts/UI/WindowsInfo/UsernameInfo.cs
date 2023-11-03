using Additional.Game;
using TMPro;
using UnityEngine;

namespace UI.WindowsInfo
{
    public class UsernameInfo : MonoSingleton<UsernameInfo>
    {
        [SerializeField] private TMP_InputField _usernameField;

        public string Username => _usernameField.text;
    }
}